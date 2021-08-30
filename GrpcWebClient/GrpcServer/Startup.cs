using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();

            services.AddCors(cors =>
            {
                cors.AddDefaultPolicy(builder =>
                {
                    /* Access-Control-Allow-Credentials can't be used with wildcard origins */
                    // builder.WithOrigins("grpc.local")
                    //     .AllowCredentials();

                    builder.WithOrigins("*")
                       .WithMethods("POST", "OPTIONS")
                       .WithHeaders("authorization", "keep-alive", "user-agent", "cache-control", "content-type", "content-transfer-encoding", "x-accept-content-transfer-encoding", "x-accept-response-streaming", "x-user-agent", "x-grpc-web", "grpc-timeout", "x-client-id")
                       .WithExposedHeaders("grpc-status", "grpc-message", "grpc-encoding", "grpc-accept-encoding");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseGrpcWeb();
            app.UseCors();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb();
            //    endpoints.MapGrpcService<PhotoService>().EnableGrpcWeb();
            //    endpoints.MapControllers();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb(); 

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
