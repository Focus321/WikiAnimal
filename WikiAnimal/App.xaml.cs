using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using WikiAnimal.Domain;
using WikiAnimal.Domain.Repository;
using WikiAnimal.Services;

namespace WikiAnimal
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigurationServiceAsync(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }
        private void ConfigurationServiceAsync(IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(TypeOfAnimalRepository));
            services.AddTransient(typeof(AnimalRepository));
            services.AddDbContext<AnimalDatabaseContext>(option => option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AnimalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddTransient(typeof(AnimalDatabaseContext));
            services.AddTransient(typeof(AnimalWikiServices));
        }
    }
}