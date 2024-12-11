using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestWithValue.Application.AllServicesAndInterfaces.Services;
using TestWithValue.Application.AllServicesAndInterfaces.Services.OperationResultService;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.ActionMessage_s_Interface;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface.OperationResult__s_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Data;
using TestWithValue.Data.Repository;
using TestWithValue.Domain.Enitities;
using TestWithValue.Persistence.Repositories;

namespace TestWithValue.Infrastructure.IOC
{
    public static class ConfigureRegistration
    {
        public static void ConfigurePersistenceServices(this IServiceCollection services,
    IConfiguration configuration)
        {
            services.AddDbContext<TestWithValueDbContext>(options =>
            {
                options.UseSqlServer(configuration
                    .GetConnectionString("TestWithValueConnection"));
            });


            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ITestService, TestService>();

            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionService, QuestionService>();

            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IOptionService, OptionService>();

            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IAnswerService, AnswerService>();

            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ITopicSevice, TopicSevice>();

            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<ICartItemService, CartItemService>();

            services.AddScoped<IActionMessageService, TempDataActionMessageService>();
            services.AddScoped<IOperationResultService, OperationResultService>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITempDataDictionaryFactory, TempDataDictionaryFactory>();

            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();
            
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<TestWithValueDbContext>()
                    .AddDefaultTokenProviders();
           
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserSevice, UserService>();
		
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
			services.AddScoped<IProvinceService, ProvinceService>();


			services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();

			services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationService, OrganizationService>();

            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IUserInfoService, UserInfoService>();


            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<ICaseService, CaseService>();


            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped<ISuggestedCaseRepository, SuggestedCaseRepository>();











            services.AddAutoMapper(Assembly.GetExecutingAssembly());


        }
    }
}
