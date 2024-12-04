using Nsu.HackathonProblem.Contracts;

namespace Hackathon.Strategy;

public class StableMarriageTeamBuildingStrategy : ITeamBuildingStrategy
{
    public IEnumerable<Team> BuildTeams(IEnumerable<Employee> teamLeads, IEnumerable<Employee> juniors,
        IEnumerable<Wishlist> teamLeadsWishlists, IEnumerable<Wishlist> juniorsWishlists)
    {
        
        var teamLeadsList = teamLeads.ToList();
        var juniorsList = juniors.ToList();

        var teamLeadsWishlistDict = teamLeadsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        var juniorsWishlistDict = juniorsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        
        var teamLeadFree = new HashSet<int>(teamLeadsList.Select(t => t.Id));
        var juniorEngaged = new Dictionary<int, int>();
        
        var proposals = teamLeadsList.ToDictionary(t => t.Id, t => 0);
        
        while (teamLeadFree.Count != 0)
        {
            var teamLeadId = teamLeadFree.First();
            teamLeadFree.Remove(teamLeadId);
            
            var teamLeadPreferences = teamLeadsWishlistDict[teamLeadId];
            var preferredJuniorId = teamLeadPreferences[proposals[teamLeadId]];
            proposals[teamLeadId]++;
            
            if (!juniorEngaged.ContainsKey(preferredJuniorId))
            {
                juniorEngaged[preferredJuniorId] = teamLeadId;
            }
            else
            {
                var currentTeamLead = juniorEngaged[preferredJuniorId];
                var juniorPreferences = juniorsWishlistDict[preferredJuniorId].ToList();
                
                if (juniorPreferences.IndexOf(teamLeadId) < juniorPreferences.IndexOf(currentTeamLead))
                {
                    juniorEngaged[preferredJuniorId] = teamLeadId;
                    teamLeadFree.Add(currentTeamLead);
                }
                else
                {
                    teamLeadFree.Add(teamLeadId);
                }
            }
        }
        var teams = new List<Team>();

        foreach (var pair in juniorEngaged)
        {
            var junior = juniorsList.First(j => j.Id == pair.Key);
            var teamLead = teamLeadsList.First(t => t.Id == pair.Value);
            teams.Add(new Team(teamLead, junior));
        }

        return teams;
    }
}
