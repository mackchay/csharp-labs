using Hackathon.Strategy;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon;

public class Hackathon
{
    private const string ResourceDirectory = "Resources";
    private const string JuniorsFileName = "Juniors20.csv";
    private const string TeamLeadsFileName = "Teamleads20.csv";
    public List<Employee> TeamLeads { get; set; }
    public List<Employee> Juniors { get; set; }
    public List<Wishlist> TeamLeadsWishlists { get; set; }
    public List<Wishlist> JuniorsWishlists { get; set; }
    public IEnumerable<Team> Teams { get; set; }

    public Hackathon()
    {
        var resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ResourceDirectory);
        var parser = new EmployeeParser(resourcesPath);
        TeamLeads = parser.Parse(TeamLeadsFileName);
        Juniors = parser.Parse(JuniorsFileName);
        TeamLeadsWishlists = WishlistGenerator.Generate(TeamLeads, Juniors);
        JuniorsWishlists = WishlistGenerator.Generate(Juniors, TeamLeads);
    }

    public Hackathon(List<Employee> teamLeads, List<Employee> juniors, 
        List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        TeamLeads = teamLeads;
        Juniors = juniors;
        TeamLeadsWishlists = teamLeadsWishlists;
        JuniorsWishlists = juniorsWishlists;
    }
    
    public double Start()
    {
            var hrManager = new HrManager(new StableMarriageTeamBuildingStrategy());
            Teams = hrManager.MakeTeams(TeamLeads, Juniors, TeamLeadsWishlists, JuniorsWishlists);
            var hrDirector = new HrDirector();
            var harmony = hrDirector.CalculateHarmony(Teams.ToList(), TeamLeadsWishlists, JuniorsWishlists);
            return harmony;
    }
}