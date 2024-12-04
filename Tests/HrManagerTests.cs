using Hackathon;
using Hackathon.Strategy;
using Moq;
using Nsu.HackathonProblem.Contracts;

namespace Tests;

public class HrManagerTests
{
    [Test]
    public void HrManagerTeamsCountTest()
    {
        var teamLeads = new List<Employee>
        {
            new Employee(101, "John"),
            new Employee(202, "Jane"),
            new Employee(303, "Doe")
        };
        var juniors = new List<Employee>
        {
            new Employee(111, "John"),
            new Employee(222, "Jane"),
            new Employee(333, "Doe")
        };
        var teamLeadsWishlists = WishlistGenerator.Generate(teamLeads, juniors);
        var juniorsWishlists = WishlistGenerator.Generate(juniors, teamLeads);
        var hrManager = new HrManager(new StableMarriageTeamBuildingStrategy());
        var teams = hrManager.MakeTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists).ToList();
        Assert.That(teams, Has.Count.EqualTo(teamLeads.Count));
    }

    [Test]
    public void HrManagerStrategyTest()
    {
        var teamLeads = new List<Employee>
        {
            new Employee(101, "John"),
            new Employee(202, "Jane"),
            new Employee(303, "Doe")
        };
        var juniors = new List<Employee>
        {
            new Employee(111, "John"),
            new Employee(222, "Jane"),
            new Employee(333, "Doe")
        };
        var teamLeadsWishlists = new List<Wishlist>
        {
            new Wishlist(101, new int[] { 111, 222, 333 }),
            new Wishlist(202, new int[] { 111, 222, 333 }),
            new Wishlist(303, new int[] { 111, 222, 333 }),
        };
        var juniorsWishlists = new List<Wishlist>
        {
            new Wishlist(111, new int[] { 101, 202, 303 }),
            new Wishlist(222, new int[] { 101, 202, 303 }),
            new Wishlist(333, new int[] { 101, 202, 303 }),
        };
        var hrManager = new HrManager(new StableMarriageTeamBuildingStrategy());
        var teams = hrManager.MakeTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists).ToList();
        var expectedTeams = new List<Team>
        {
            new Team(teamLeads[0], juniors[0]),
            new Team(teamLeads[1], juniors[1]),
            new Team(teamLeads[2], juniors[2])
        };
        Assert.That(teams, Is.EqualTo(expectedTeams));
    }

    [Test]
    public void HrManagerStrategyCalledOnceTest()
    {
        var teamLeads = new List<Employee>
        {
            new Employee(101, "John"),
            new Employee(202, "Jane"),
            new Employee(303, "Doe")
        };
        var juniors = new List<Employee>
        {
            new Employee(111, "John"),
            new Employee(222, "Jane"),
            new Employee(333, "Doe")
        };
        var teamLeadsWishlists = new List<Wishlist>
        {
            new Wishlist(101, new int[] { 111, 222, 333 }),
            new Wishlist(202, new int[] { 111, 222, 333 }),
            new Wishlist(303, new int[] { 111, 222, 333 }),
        };
        var juniorsWishlists = new List<Wishlist>
        {
            new Wishlist(111, new int[] { 101, 202, 303 }),
            new Wishlist(222, new int[] { 101, 202, 303 }),
            new Wishlist(333, new int[] { 101, 202, 303 }),
        };
        
        var mockService = new Mock<ITeamBuildingStrategy>();
        var hrManager = new HrManager(mockService.Object);

        hrManager.MakeTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
        
        mockService.Verify(s => s.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists), 
            Times.Once);
    }
}