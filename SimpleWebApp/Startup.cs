using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using SimpleWebApp.Classes;
using Microsoft.AspNetCore.Identity;

using static System.Diagnostics.Debug;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SimpleWebApp.Repository;

namespace SimpleWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPredictionRepository, PredictionDatabseRepository>();
            services.AddSingleton<IUserRepository, UserDatabseRepository>();
            services.AddSingleton<PredictionsManager>();
            services.AddSingleton<UserManager>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = new PathString("/auth"));
            services.AddAuthorization();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute("default", "{controller}/{action}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    if (context.User.Identity.Name == null) { await context.Response.WriteAsync(File.ReadAllText(@"Site/predictionsPage.html")); return; }
                        
                    var user = app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles;

                    if (user == CredentialsDto.Admin) await context.Response.WriteAsync(File.ReadAllText(@"Site/predictionsPageAdmin.html"));
                    else await context.Response.WriteAsync(File.ReadAllText(@"Site/predictionsPageUser.html"));
                });

                endpoints.MapGet("/auth", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(@"Site/loginPage.html"));
                });

                endpoints.MapGet("/registrationPage", async context =>
                {
                    if (context.User.Identity.Name == null) await context.Response.WriteAsync(File.ReadAllText(@"Site/registrationPage.html"));
                });

                endpoints.MapPost("/registration", async context =>
                {
                    if (context.User.Identity.Name == null)
                    {
                        var credentials = await context.Request.ReadFromJsonAsync<Credential>();
                        credentials.Roles = CredentialsDto.User;

                        if (!app.ApplicationServices.GetService<UserManager>().GetExist(credentials))
                            app.ApplicationServices.GetService<UserManager>().AddUser(credentials);
                        else
                            await context.Response.WriteAsync("no");
                    }
                });

                endpoints.MapPost("/login", async context =>
                {
                    var credentials = await context.Request.ReadFromJsonAsync<Credential>();

                    if (app.ApplicationServices.GetService<UserManager>().IsCorrectParams(credentials))
                    {
                        List<Claim> claims = new()
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, credentials.Login)
                        };

                        ClaimsIdentity id = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                        context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)).GetAwaiter().GetResult();

                        if (context.User.Identity.Name != null)
                        {
                            if (app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                                await context.Response.WriteAsync("go");
                            else
                                await context.Response.WriteAsync("ok");
                        }
                        else
                            await context.Response.WriteAsync("not_auth");
                    }
                    else { await context.Response.WriteAsync("not_auth"); }
                });

                /*endpoints.MapGet("/adminPage", async context =>
                {
                    var user = app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles;
                    if (user == CredentialsDto.Admin) await context.Response.WriteAsync(File.ReadAllText(@"Site/adminPage.html"));
                    else context.Response.StatusCode = 404;
                }).RequireAuthorization();*/

                endpoints.MapGet("/answersPage", async context =>
                {
                    if (context.User.Identity.Name != null && app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                        await context.Response.WriteAsync(File.ReadAllText(@"Site/answersPage.html"));
                    else 
                        context.Response.StatusCode = 404;
                }).RequireAuthorization();

                endpoints.MapGet("/info", async context =>
                {
                    if (context.User.Identity.Name != null && app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                        await context.Response.WriteAsync("<h8><b>Info</b></h8></br><a href=\"../\">Go to start</a></br><a href=\"../adminPage\">Go to admin page</a></br><a href=\"../answersPage\">Go to answers page</a></br><a href=\"../predictionsPage\">Go to predictions page</a>");
                    else 
                        context.Response.StatusCode = 404;
                }).RequireAuthorization();

                endpoints.MapGet("/text", async context =>
                {
                    if (context.User.Identity.Name != null && app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                    {
                        var res = context.Request.Headers[":path"][0].Split('?')[1];
                        Dictionary<string, string> paramentars = new();
                        foreach (var item in res.Split(',')) { paramentars.Add(item.Split('=')[0], item.Split('=')[1]); }

                        await context.Response.WriteAsync(paramentars.ContainsKey("text") ? paramentars["text"] : "Ошибка");
                    }
                    else 
                        context.Response.StatusCode = 404;
                }).RequireAuthorization();

                endpoints.MapGet("/randomPrediction", async context =>
                {
                    await context.Response.WriteAsync(app.ApplicationServices.GetService<PredictionsManager>().GetRandomPrediction());
                });

                /*endpoints.MapGet("/gethtmlPredictions", async context =>
                {
                    if (context.User.Identity.Name != null && app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                    {
                        string output = "<table><tr><td><center><h7 style=\"color: white\">Number</h7></center></td><td><center><h7 style=\"color: white\">Prediction</h7></center></td><td><center><h7 style=\"color: white\">Edit</h7></center></td><td><center><h7 style=\"color: white\">Delete</h7></center></td></tr>";

                        int num = 0;
                        foreach (var item in app.ApplicationServices.GetService<PredictionsManager>().GetAllPredictions())
                        {
                            output += $"<tr><td><center><h7 style=\"color: white\">{num + 1}</h7></center></td><td><input type=\"text\" id=\"{item.Id}\" value=\"{item.PredictionString}\" style=\"color: black\">" + $"</td><td><center><button style=\"color: black\" onclick=\"Edit({item.Id})\">✎</button></center></td><td><center><button style=\"color: red\" onclick=\"Delete({item.Id})\">✘</button></center></td></tr>";
                            num++;
                        }

                        output += "</table>";

                        await context.Response.WriteAsync(output);
                    }
                    else 
                        context.Response.StatusCode = 404;
                }).RequireAuthorization();*/

                endpoints.MapGet("/getPredictions", async context =>
                {
                    if (context.User.Identity.Name != null && app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                        await context.Response.WriteAsJsonAsync(app.ApplicationServices.GetService<PredictionsManager>().GetAllPredictions());
                    else 
                        context.Response.StatusCode = 404;
                }).RequireAuthorization();

                /*endpoints.MapPost("/addPrediction", async context =>
                {
                    if (context.User.Identity.Name != null && app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                    {
                        if (!context.Request.HasJsonContentType())
                        {
                            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                            return;
                        }

                        app.ApplicationServices.GetService<PredictionsManager>().AddPrediction((await context.Request.ReadFromJsonAsync<Prediction>()).PredictionString);
                    }
                    else 
                        context.Response.StatusCode = 404;
                }).RequireAuthorization();*/

                /*endpoints.MapPost("/deletePrediction", async context =>
                {
                    if (context.User.Identity.Name != null && app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                    {
                        if (!context.Request.HasJsonContentType())
                        {
                            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                            return;
                        }

                        app.ApplicationServices.GetService<PredictionsManager>().RemovePrediction((await context.Request.ReadFromJsonAsync<Prediction>()).Id);
                    }
                    else 
                        context.Response.StatusCode = 404;
                }).RequireAuthorization();*/

                endpoints.MapPut("/editPrediction", async context =>
                {
                    if (context.User.Identity.Name != null && app.ApplicationServices.GetService<UserManager>().GetUser(context.User.Identity.Name).Roles == CredentialsDto.Admin)
                    {
                        if (!context.Request.HasJsonContentType())
                        {
                            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                            return;
                        }

                        var query = await context.Request.ReadFromJsonAsync<Prediction>();

                        app.ApplicationServices.GetService<PredictionsManager>().Edit(query.Id, query.PredictionString);
                    }
                    else 
                        context.Response.StatusCode = 404;
                }).RequireAuthorization();
            });
        }
    }
}
