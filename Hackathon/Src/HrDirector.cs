using Nsu.HackathonProblem.Contracts;

namespace Hackathon;

public class HrDirector
{
    public double CalculateHarmony(List<Team> teams, List<Wishlist> teamLeadsWishlists, 
        List<Wishlist> juniorsWishlists)
    {
        return Calculate(teams, teamLeadsWishlists, juniorsWishlists);
    }
    
    private static double Calculate(List<Team> teams, List<Wishlist> teamLeadsWishlists, 
        List<Wishlist> juniorsWishlists)
    {
        
        var teamLeadsWishlistDict = teamLeadsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        var juniorsWishlistDict = juniorsWishlists.ToDictionary(w => w.EmployeeId, w => w.DesiredEmployees);
        
        var teamLeadSatisfaction = new Dictionary<int, int>();
        var juniorSatisfaction = new Dictionary<int, int>();

        foreach (var wishlist in teamLeadsWishlists)
        {
            teamLeadSatisfaction[wishlist.EmployeeId] = 0;
        }
        foreach (var wishlist in juniorsWishlists)
        {
            juniorSatisfaction[wishlist.EmployeeId] = 0;
        }
        
        
        foreach (var team in teams)
        {
            var teamLeadId = team.TeamLead.Id;
            var juniorId = team.Junior.Id;

            var teamLeadPreference = teamLeadsWishlistDict[teamLeadId];
            var juniorPreference = juniorsWishlistDict[juniorId];
            
            var teamLeadSatisfactionIndex = teams.Count - teamLeadPreference.ToList().IndexOf(juniorId);
            teamLeadSatisfaction[teamLeadId] += teamLeadSatisfactionIndex;
            
            var juniorSatisfactionIndex = teams.Count - juniorPreference.ToList().IndexOf(teamLeadId);
            juniorSatisfaction[juniorId] += juniorSatisfactionIndex;
        }
        
        var averageHarmonicSatisfaction = HarmonyCalculator.HarmonicMeanSatisfaction(teamLeadSatisfaction.Values.Concat(juniorSatisfaction.Values));

        return averageHarmonicSatisfaction;
    } 
}