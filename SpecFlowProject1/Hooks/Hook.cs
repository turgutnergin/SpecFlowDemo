using System;
using BoDi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SpecFlowProject1.Repository;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.Hooks
{
    [Binding]
    public class Hooks
    {
        private const string AppSettingsFile = "../../../appsettings.json";

        private readonly IObjectContainer _objectContainer;
        
        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }
        
        [BeforeScenario]
        public async Task RegisterServices()
        {
            var client = new HttpClient();
            _objectContainer.RegisterInstanceAs(client);

            var factory = GetWebApplicationFactory();
            _objectContainer.RegisterInstanceAs(factory);
            
            var jsonRepository = new JsonFilesRepository();
            _objectContainer.RegisterInstanceAs(jsonRepository);
        }

        private WebApplicationFactory<Program> GetWebApplicationFactory() =>
            new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    IConfigurationSection? configSection = null;
                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), AppSettingsFile));
                    });
                });
        
    }
}