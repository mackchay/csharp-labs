using Hackathon;
using Hackathon.Strategy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nsu.HackathonProblem.Contracts;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<HackathonWorker>(); 
        services.AddTransient<Hackathon.Hackathon>();
        services.AddTransient<HrManager>();
        services.AddTransient<HrDirector>(); 
        services.AddTransient<ITeamBuildingStrategy, StableMarriageTeamBuildingStrategy>();
        services.AddSingleton<EmployeeParser>();
    })
    .Build();

await host.StartAsync();
await Task.Delay(5000);
await host.StopAsync();






