    using Azure.Storage.Blobs;
using BussinessLogic.Models;
using BussinessLogic.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Microsoft.VisualBasic.FileIO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TimeCapsuleBackend.Data.Models;
    using TimeCapsuleBackend.Data.Repository;
    using TimeCapsuleBackend.Data.Repository.IRepository;
    using TimeCapsuleBackend.Helper;

    namespace TimeCapsuleBackend
    {
        public class Startup
        {
            public IConfiguration Configuration { get; }

            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }


            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                    AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["JWT:Issuer"],
                            ValidAudience = Configuration["JWT:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                        };
                    });

                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins("https://localhost:44351", "http://localhost:5173")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                        });
                });

                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = "localhost:6379";
                    options.InstanceName = "SampleInstance";
                });


                services.AddControllers();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeCapsuleBackend", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
                });

                // Register TImeCapsuleDBContext with explicit constructor selection
                services.AddDbContext<TImeCapsuleDBContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("TimeCapsuleDB"));
                });

                services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

                services.AddTransient<IMailService, MailService>();
                services.AddScoped<ITimeCapsuleEmailRepository, TimeCapsuleEmailRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
                services.AddScoped<ITimeCapsuleRepository, TimeCapsuleRepository>();
                services.AddScoped<ITCContentRepository, TCContentRepository>();
                services.AddSingleton(x => new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorage")));
                services.AddTransient<IBlobService, BlobService>();
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                app.UseDeveloperExceptionPage();

                if (env.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeCapsuleBackend v1"));
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseCors();

                app.UseAuthentication();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    }
