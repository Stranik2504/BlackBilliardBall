using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using SimpleWebApp.Classes;
using Microsoft.AspNetCore.Identity;

using static System.Diagnostics.Debug;

namespace SimpleWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PredictionsManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(@"Site/startPage.html"));
                });

                endpoints.MapGet("/adminPage", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(@"Site/adminPage.html"));
                });

                endpoints.MapGet("/answersPage", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(@"Site/answersPage.html"));
                });

                endpoints.MapPost("/login", async context =>
                {
                    var user = await context.Request.ReadFromJsonAsync<User>();
                    if (user.Login == "admin" && user.Password == "admin") { context.Response.StatusCode = StatusCodes.Status202Accepted; }
                    else { context.Response.StatusCode = StatusCodes.Status203NonAuthoritative; }
                    await context.Response.CompleteAsync();
                });

                endpoints.MapGet("/predictionsPage", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(@"Site/predictionsPage.html"));
                });

                endpoints.MapGet("/info", async context =>
                {
                    await context.Response.WriteAsync("<h8><b>Info</b></h8></br><a href=\"../\">Go to start</a></br><a href=\"../adminPage\">Go to admin page</a></br><a href=\"../answersPage\">Go to answers page</a></br><a href=\"../predictionsPage\">Go to predictions page</a>");
                });

                endpoints.MapGet("/text", async context =>
                {
                    var res = context.Request.Headers[":path"][0].Split('?')[1];
                    Dictionary<string, string> paramentars = new Dictionary<string, string>();
                    foreach (var item in res.Split(',')) { paramentars.Add(item.Split('=')[0], item.Split('=')[1]); }

                    await context.Response.WriteAsync(paramentars.ContainsKey("text") ? paramentars["text"] : "Ошибка");
                });

                endpoints.MapGet("/randomPrediction", async context =>
                {
                    await context.Response.WriteAsync(app.ApplicationServices.GetService<PredictionsManager>().GetRandomPrediction());
                });

                endpoints.MapGet("/getPredictions", async context =>
                {
                    string output = "<table><tr><td><center><h7 style=\"color: white\">Number</h7></center></td><td><center><h7 style=\"color: white\">Prediction</h7></center></td><td><center><h7 style=\"color: white\">Edit</h7></center></td><td><center><h7 style=\"color: white\">Delete</h7></center></td></tr>";

                    int num = 0;
                    foreach (var item in app.ApplicationServices.GetService<PredictionsManager>().GetAllPredictions())
                    {
                        output += $"<tr><td><center><h7 style=\"color: white\">{num + 1}</h7></center></td><td><input type=\"text\" id=\"{num}\" value=\"{item}\" style=\"color: black\">" + $"</td><td><center><button style=\"color: black\" onclick=\"Edit({num})\">✎</button></center></td><td><center><button style=\"color: red\" onclick=\"Delete('{item}')\">✘</button></center></td></tr>";
                        num++;
                    }

                    output += "</table>";

                    await context.Response.WriteAsync(output);
                });

                endpoints.MapPost("/addPrediction", async context =>
                {
                    if (!context.Request.HasJsonContentType())
                    {
                        context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                        return;
                    }

                    app.ApplicationServices.GetService<PredictionsManager>().AddPrediction((await context.Request.ReadFromJsonAsync<Prediction>()).PredictionString);
                });

                endpoints.MapPost("/deletePrediction", async context =>
                {
                    if (!context.Request.HasJsonContentType())
                    {
                        context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                        return;
                    }

                    app.ApplicationServices.GetService<PredictionsManager>().RemovePrediction((await context.Request.ReadFromJsonAsync<Prediction>()).PredictionString);
                });

                endpoints.MapPut("/editPrediction", async context =>
                {
                    if (!context.Request.HasJsonContentType())
                    {
                        context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                        return;
                    }

                    var query = await context.Request.ReadFromJsonAsync<EditPrediction>();

                    app.ApplicationServices.GetService<PredictionsManager>().Edit(query.NumPresiction, query.PredictionString);
                });
            });
        }
    }
}
