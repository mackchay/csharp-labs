using Hackathon.Strategy;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon;

public class HrManager(ITeamBuildingStrategy strategy)
{
    
    public IEnumerable<Team> MakeTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        var teams = strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        return teams;
    }
    
}