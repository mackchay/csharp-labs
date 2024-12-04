using Hackathon;
using Nsu.HackathonProblem.Contracts;

namespace Tests;

public class WishlistTests
{

    [Test]
    public void WishlistSizeTest()
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
        Assert.That(teamLeadsWishlists.Count, Is.EqualTo(juniors.Count));
        Assert.That(juniorsWishlists.Count, Is.EqualTo(teamLeads.Count));
    }

    [Test]
    public void WishlistEmployeeTest()
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
        for (var i = 0; i < teamLeads.Count; i++)
        {
            for (var j = 0; j < teamLeads.Count; j++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(teamLeadsWishlists[j].DesiredEmployees.ToList(), Does.Contain(juniors[i].Id));
                    Assert.That(juniorsWishlists[j].DesiredEmployees.ToList(), Does.Contain(teamLeads[i].Id));
                });
            }
        }
    }
}