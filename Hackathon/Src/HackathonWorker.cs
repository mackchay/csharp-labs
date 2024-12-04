using Hackathon.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hackathon;

public class HackathonWorker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;


    public HackathonWorker(IServiceProvider services)
    {
        _serviceProvider = services;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("HackathonWorker started.");
        double harmony = 0f;
        for (var i = 0; i < 1000; i++)
        {
            var hackathon = _serviceProvider.GetService<Hackathon>();
            harmony += hackathon.Start();
        }

        Console.WriteLine($"Harmony: {harmony}");
        return Task.CompletedTask;
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("HackathonWorker stopped.");
        return Task.CompletedTask;
    }

    public void SaveHackathonData(Hackathon hackathon)
    {
        using (var context = new HackathonContext())
        {
            // Сохранение сотрудников
            foreach (var teamLead in hackathon.TeamLeads)
            {
                var teamLeadEntity = new EmployeeEntity
                {
                    Name = teamLead.Name,
                    Role = Role.TeamLead
                };
                context.Employees.Add(teamLeadEntity);
            }

            foreach (var junior in hackathon.Juniors)
            {
                var juniorEntity = new EmployeeEntity
                {
                    Name = junior.Name,
                    Role = Role.Junior
                };
                context.Employees.Add(juniorEntity);
            }

            context.SaveChanges(); // Сохраняем сотрудников перед сохранением связей с ними

            // Сохранение списков предпочтений тимлидов
            foreach (var wishlist in hackathon.TeamLeadsWishlists)
                for (var i = 0; i < wishlist.DesiredEmployees.Length; i++)
                {
                    var wishlistEntity = new WishlistEntity
                    {
                        EmployeeId = context.Employees
                            .FirstOrDefault(e => e.Id == wishlist.DesiredEmployees. && 
                                                 e.Role == Role.TeamLead).Id,
                        PreferredEmployeeId = context.Employees
                            .FirstOrDefault(e => e.Name == wishlist.DesiredEmployees[i]. && 
                                                 e.Role == Role.Junior).Id,
                        Rank = i + 1,
                        HackathonId = wishlist.HackathonId
                    };
                    context.Wishlists.Add(wishlistEntity);
                }

            // Сохранение списков предпочтений джунов
            foreach (var wishlist in hackathon.JuniorsWishlists)
                for (var i = 0; i < wishlist.DesiredEmployees.Length; i++)
                {
                    var wishlistEntity = new WishlistEntity
                    {
                        EmployeeId = context.Employees
                            .FirstOrDefault(e => e.Name == wishlist.Employee.Name && e.Role == "Junior").Id,
                        PreferredEmployeeId = context.Employees
                            .FirstOrDefault(e => e.Name == wishlist.Preferences[i].Name && e.Role == "TeamLead").Id,
                        Rank = i + 1,
                        HackathonId = wishlist.HackathonId
                    };
                    context.Wishlists.Add(wishlistEntity);
                }

            // Сохранение команд
            foreach (var team in teams)
            {
                var teamEntity = new TeamEntity
                {
                    Junior = context.Employees.FirstOrDefault(e => e.Name == team.Junior.Name && e.Role == "Junior"),
                    TeamLead = context.Employees.FirstOrDefault(e =>
                        e.Name == team.TeamLead.Name && e.Role == "TeamLead"),
                    HackathonId = team.HackathonId
                };
                context.Teams.Add(teamEntity);
            }

            // Сохранение хакатона
            var hackathonEntity = new HackathonEntity
            {
                Date = DateTime.Now,
                Teams = teams.Select(t => new TeamEntity
                {
                    Junior = context.Employees.FirstOrDefault(e => e.Name == t.Junior.Name && e.Role == Role.Junior),
                    TeamLead = context.Employees.FirstOrDefault(e => e.Name == t.TeamLead.Name && e.Role == Role.TeamLead)
                }).ToList(),
                HarmonyScore = hackathon.Start()
            };
            context.Hackathons.Add(hackathonEntity);

            context.SaveChanges();
        }
    }

    public Task StartHackathonAsync()
    {
        var hackathon = new Hackathon();
        var hackathonMapper = new HackathonMapper();
        var harmonyScore = hackathon.Start();
        var context = new HackathonContext();
        var hackathonEntity = new HackathonEntity
        {
            Date = DateTime.Now,
            HarmonyScore = harmonyScore
        };
        var juniorsEntities = hackathon.Juniors.Select(x =>
            hackathonMapper.MapToEntity(x, hackathonEntity, Role.Junior)
        );
        var teamsLeads = hackathon.TeamLeads.Select(x =>
            hackathonMapper.MapToEntity(x, hackathonEntity, Role.TeamLead)
        );

        context.Add(hackathonEntity);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}