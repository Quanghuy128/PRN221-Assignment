using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Repository;
using Repository.CarInformationRepo.Commands.CreateCarCommand;
using Repository.CarInformationRepo.Commands.DeleteCarCommand;
using Repository.CarInformationRepo.Commands.UpdateCarCommand;
using Repository.CarInformationRepo.Queries.GetCarsQuery;
using Repository.CustomerRepo;
using Repository.CustomerRepo.Commands.CreateCustomerCommand;
using Repository.CustomerRepo.Commands.DeleteCustomerCommand;
using Repository.CustomerRepo.Commands.UpdateCustomerCommand;
using Repository.CustomerRepo.Queries.GetCustomersQuery;
using Repository.CustomerRepo.Queries.LoginQuery;
using Repository.RentingTransactionRepo.Queries.GetRentingTransactionCustomerQuery;
using Repository.RentingTransactionRepo.Queries.GetRentingTransactionDetailQuery;
using System;
using System.IO;
using System.Windows;

namespace NguyenQuangHuyWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public IServiceCollection Services { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            }) 
            .ConfigureServices((hostContext, services) =>
            {
                ConfigureServices(hostContext.Configuration, services);
            }).Build();
        }

        private void ConfigureServices(IConfiguration configuration,
        IServiceCollection services)
        {
            var adminAccountSection = configuration.GetSection("Admin").Get<AdminAccount>();
            services.AddSingleton(_ => adminAccountSection);

            services.AddDbContext<FucarRentingManagementContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("LocalDB"));
                x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                x.EnableSensitiveDataLogging(true);
            }, ServiceLifetime.Singleton);

            services.AddScoped(typeof(IGenericRepository<>), typeof(EfRepository<>));

            services.AddScoped<Login>();
            services.AddScoped<CustomerView>();
            services.AddScoped<CustomerCreate>();
            services.AddScoped<CustomerUpdate>();

            services.AddScoped<CarManagementView>();
            services.AddScoped<CarCreate>();

            services.AddScoped<RentingTransactionView>();

            services.AddScoped<CustomerProfile>();
            services.AddScoped<TrackTransaction>();

            services.AddScoped<IService, Service>();

            services.AddScoped<ILoginQuery, LoginQuery>();
            services.AddScoped<IGetCustomersQuery, GetCustomersQuery>();
            services.AddScoped<ICreateCustomerCommand, CreateCustomerCommand>();
            services.AddScoped<IUpdateCustomerCommand, UpdateCustomerCommand>();
            services.AddScoped<IDeleteCustomerCommand, DeleteCustomerCommand>();

            services.AddScoped<IGetCarQuery, GetCarQuery>();
            services.AddScoped<ICreateCarCommand, CreateCarCommand>();
            services.AddScoped<IUpdateCarCommand, UpdateCarCommand>();
            services.AddScoped<IDeleteCarCommand, DeleteCarCommand>();

            services.AddScoped<IGetRentingTransactionDetailQuery, GetRentingTransactionDetailQuery>();
            services.AddScoped<IGetRentingTransactionCustomerQuery, GetRentingTransactionCustomerQuery>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<Login>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
