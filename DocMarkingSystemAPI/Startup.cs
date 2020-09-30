using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using DI;
using DIContract;
using DocMarkingSystemContracts.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DocMarkingSystemAPI
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //resolver
            string path = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "dlls");
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dlls");
            var resolver = new Resolver(path, services);
            services.AddSingleton<IResolver>(sp => resolver);
            //controllers
            services.AddControllers();
            //swagger
            services.AddSwaggerGen();

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //web socket
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/ws/marker"))
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<IMarkerWebSocket>();
                        await handler.onConnected(webSocket);
                        var recive = await webSocket.ReceiveAsync(new Memory<byte>(), CancellationToken.None);
                        if (recive.MessageType == WebSocketMessageType.Close)
                        {
                            await handler.onDisconnected(webSocket);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }else if (context.Request.Path.StartsWithSegments("/ws/view"))
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        string userID = context.Request.Query["id"];
                        string docID = context.Request.Query["docID"];
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<IViewWebSocket>();
                        await handler.onConnected(webSocket, userID,docID);
                        var recive = await webSocket.ReceiveAsync(new Memory<byte>()
                            , CancellationToken.None);
                        if (recive.MessageType == WebSocketMessageType.Close)
                        {
                            await handler.onDisconnected(webSocket,userID,docID);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else if (context.Request.Path.StartsWithSegments("/ws/liveDraw"))
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        string userID = context.Request.Query["id"];
                        string docID = context.Request.Query["docID"];
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<ILiveDrawWebSocket>();
                        await handler.onConnected(webSocket, userID, docID);
                        
                        while (true)
                        {
                            var buffer = new byte[4082];
                            var recive = await webSocket.ReceiveAsync(new Memory<byte>(buffer)
                                , CancellationToken.None);
                            if (recive.MessageType == WebSocketMessageType.Close)
                            {
                                await handler.onDisconnected(webSocket, userID, docID);
                                break;
                            }
                            else
                            {
                                await handler.Receive(webSocket, userID, docID, buffer);
                            }
                        }
                        
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else if (context.Request.Path.StartsWithSegments("/ws/document"))
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        string userID = context.Request.Query["id"];
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<IDocumentWebSocket>();
                        await handler.onConnected(webSocket,userID);
                        var recive = await webSocket.ReceiveAsync(new Memory<byte>()
                            , CancellationToken.None);
                        if (recive.MessageType == WebSocketMessageType.Close)
                        {
                            await handler.onDisconnected(webSocket);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }else if (context.Request.Path == "/ws/sharing")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var handler = app.ApplicationServices.GetService<ISharingWebSocket>();
                        await handler.onConnected(webSocket);
                        var recive = await webSocket.ReceiveAsync(new Memory<byte>(), CancellationToken.None);
                        if (recive.MessageType == WebSocketMessageType.Close)
                        {
                            await handler.onDisconnected(webSocket);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });

            app.UseHttpsRedirection();
            //static files
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Resources")),
                RequestPath = new PathString("/Resources")
            });

            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactList API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
