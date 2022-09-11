using FluentValidation.AspNetCore;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Data;
using lbdbackend.Data.Repositories;
using lbdbackend.Service.DTOs.AccountDTOs;
using lbdbackend.Service.Exceptions;
using lbdbackend.Service.Interfaces;
using lbdbackend.Service.Mappings;
using lbdbackend.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using P225NLayerArchitectura.Service.Exceptions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace lbdbackend.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
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
                c.AddFluentValidationRulesScoped();
            });

            services.AddCors(options => options.AddDefaultPolicy(policy =>
            policy.WithOrigins("http://localhost:3000", "https://localhost:3001", "https://localhost:3002", "https://localhost:3003").AllowAnyHeader().AllowAnyMethod()
));


            //services.AddCors(options => {
            //    options.AddPolicy("ClientPermission", policy => {
            //        policy.AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .SetIsOriginAllowed(_ => true)
            //            .AllowCredentials();
            //    });
            //});
            //services.AddMvc();


            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.UseMemberCasing();
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }).AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<LoginDTOValidator>());

            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddIdentity<AppUser, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration.GetSection("JWT:Issuer").Value,
                    ValidAudience = Configuration.GetSection("JWT:Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:SecurityKey").Value)),
                };
            });

            services.AddCors(options => options.AddDefaultPolicy(policy =>
           policy.AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           .SetIsOriginAllowed(origin => true)
           ));

            services.AddAutoMapper(options => {
                options.AddProfile(new MappingProfile());
            });

            services.AddScoped<IJWTManager, JWTManager>();

            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenresService, GenresService>();

            services.AddScoped<IProfessionsService, ProfessionsService>();
            services.AddScoped<IProfessionRepository, ProfessionRepository>();

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IYearRepository, YearRepository>();
            services.AddScoped<IYearsService, YearsService>();
            services.AddScoped<IJoinMoviesGenresRepository, Data.Repositories.JoinMoviesGenresRepository>();
            services.AddScoped<IJoinMoviesPeopleRepository, JoinMoviesPeopleRepository>();

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRelationshipRepository, RelationshipRepository>();

            services.AddScoped<IMovieListService, MovieListService>();
            services.AddScoped<IMovieListRepository, MovieListRepository>();
            services.AddScoped<IJoinMoviesListsRepository, JoinMoviesListsRepository>();

            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();

            services.AddScoped<IEmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(error => {
                error.Run(async context => {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                    int statustCode = 500;
                    string errorMessage = "Internal Server Error";

                    if (feature.Error is AlreadyExistException) {
                        statustCode = 409;
                        errorMessage = feature.Error.Message;
                    }
                    else if (feature.Error is ItemNotFoundException) {
                        statustCode = 404;
                        errorMessage = feature.Error.Message;
                    }
                    else if (feature.Error is BadRequestException) {
                        statustCode = 400;
                        errorMessage = feature.Error.Message;
                    }


                    context.Response.StatusCode = statustCode;
                    await context.Response.WriteAsync(errorMessage);
                });
            });

            app.UseCors();
            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseCors("ClientPermission");



            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
