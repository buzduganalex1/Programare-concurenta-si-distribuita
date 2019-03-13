using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using FTI.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FTI.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IPublisher, ReceiptPublisher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            ////app.Use(async (context, next) =>
            ////{
            ////    if (context.Request.Path == "/ws")
            ////    {
            ////        if (context.WebSockets.IsWebSocketRequest)
            ////        {
            ////            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            ////            await Echo(context, webSocket);
            ////        }
            ////        else
            ////        {
            ////            context.Response.StatusCode = 400;
            ////        }
            ////    }
            ////    else
            ////    {
            ////        await next();
            ////    }

            ////});
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Page}/{action=Index}/{id?}");
            });
            app.UseFileServer();
            app.UseStaticFiles();
        }
        #region Echo
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        #endregion
    }
}
