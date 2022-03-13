using DruivendoosAPI.Data;
using DruivendoosAPI.Data.Repositories;
using DruivendoosAPI.Models;
using DruivendoosAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.IO;
using System.Security.Claims;

namespace DruivendoosAPI
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

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                });
            });

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,

                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:klant", policy => policy.Requirements.Add(new HasScopeRequirement("read:klant", $"https://{Configuration["Auth0:Domain"]}/")));
                options.AddPolicy("read:admin", policy => policy.Requirements.Add(new HasScopeRequirement("read:admin", $"https://{Configuration["Auth0:Domain"]}/")));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerDocument();


            //Authentication in swagger aanzetten
            services.AddOpenApiDocument(c =>
            {
                c.DocumentName = "OpenAPI 3";
                c.Title = "Druivendoos API";
                c.Version = "v1";
                c.Description = "De API voor Druivendoos website en app";
                c.AddSecurity("JWT", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey, //API keys gebruiken voor authorisatie
                    In = OpenApiSecurityApiKeyLocation.Header, //De token wordt doorgegeven via de header
                    Name = "Authorization", //Naam van de gebruikte header
                    Description = "Geef in de textbox in: Bearer {je JWT token}." //Duidelijk maken hoe je de authenticatie test in Swagger
                });

                c.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT")); //Voegt de token toe wanneer een request verstuurd wordt.
            });

            services.AddScoped<DataInitializer>();
            services.AddScoped<ICustomerWineRepository, CustomerWineRepository>();
            services.AddScoped<IWijnDoosRepository, WijnDoosRepository>();
            services.AddScoped<IWineServices, WineServices>();
            services.AddScoped<ISupplierServices, SupplierServices>();
            services.AddScoped<IBoxServices, BoxServices>();
            services.AddScoped<ICustomerServices, CustomerServices>();
            services.AddScoped<ISubscriptionServices, SubscriptionServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IInvoiceServices, InvoiceServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<IPictureServices, PictureServices>();
            services.AddScoped<IReviewServices, ReviewServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInitializer dataInit)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                dataInit.InitializeData().Wait();
            }
        }
    }
}
