using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

using static System.Diagnostics.Debug;

namespace SimpleWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
                    await context.Response.WriteAsync(File.ReadAllText(@"Site\startPage.html"));
                });

                endpoints.MapGet("/adminPage", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(@"Site\adminPage.html"));
                });

                endpoints.MapGet("/answersPage", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(@"Site\answersPage.html"));
                });

                endpoints.MapGet("/login", async context =>
                {
                    await context.Response.WriteAsync("admin");
                });

                endpoints.MapGet("/password", async context =>
                {
                    await context.Response.WriteAsync("admin");
                });

                endpoints.MapGet("/predictionsPage", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText(@"Site\predictionsPage.html"));
                });

                endpoints.MapGet("/info", async context =>
                {
                    await context.Response.WriteAsync("<h8><b>Info</b></h8></br><a href=\"../\">Go to start</a></br><a href=\"../adminPage\">Go to admin page</a></br><a href=\"../answersPage\">Go to answers page</a></br><a href=\"../predictionsPage\">Go to predictions page</a>");
                });

                /*endpoints.MapGet("/text", async context =>
                {
                    string input = "";

                    using (var streamReader = new StreamReader(context.Request.Body))
                    {
                        input = streamReader.ReadToEnd();
                    }

                    await context.Response.WriteAsync(input);
                });*/

                endpoints.MapGet("/text", async context =>
                {
                    var res = context.Request.Headers[":path"][0].Split('?')[1];
                    Dictionary<string, string> paramentars = new Dictionary<string, string>();
                    foreach (var item in res.Split(',')) { paramentars.Add(item.Split('=')[0], item.Split('=')[1]); }

                    await context.Response.WriteAsync(paramentars.ContainsKey("text") ? paramentars["text"] : "Îøèáêà");
                });

                endpoints.MapGet("/randomPrediction", async context =>
                {
                    PredictionsManager pm = new PredictionsManager();
                    await context.Response.WriteAsync(pm.GetRandomPrediction());
                });

                endpoints.MapGet("/addPrediction", async context =>
                {
                    var res = context.Request.Headers[":path"][0].Split('?')[1];
                    Dictionary<string, string> paramentars = new Dictionary<string, string>();
                    foreach (var item in res.Split(',')) { paramentars.Add(item.Split('=')[0], item.Split('=')[1]); }

                    var query = context.Request.Query;

                    PredictionsManager pm = new PredictionsManager();
                    if (paramentars.ContainsKey("text")) { pm.AddPrediction(paramentars["text"]); }

                    await context.Response.WriteAsync("OK");
                });

                /*endpoints.MapGet("/addPrediction", async context =>
                {
                    PredictionsManager pm = new PredictionsManager();
                    //var query = context.Request.Query;

                    await context.Response.WriteAsync("OK");
                });*/
            });
        }
    }
}
