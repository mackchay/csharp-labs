using Hackathon;
using Hackathon.Strategy;
using Nsu.HackathonProblem.Contracts;

namespace Tests;

public class HrDirectorTests
{
    [Test]
    public void HrDirectorAlgTest1()
    {
        const int sameNum = 10;
        var harmony = HarmonyCalculator.HarmonicMeanSatisfaction(new []
        {
            sameNum, sameNum, sameNum, sameNum
        });
        Assert.That(harmony, Is.EqualTo(sameNum));
    }

    [Test]
    public void HrDirectorAlgTest2()
    {
        var harmony = HarmonyCalculator.HarmonicMeanSatisfaction(new []
        {
            2, 6
        });
        Assert.That(harmony, Is.EqualTo(3));
    }

    [Test]
    public void HrDirectorTeamsTest()
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
        var teams = new List<Team>
        {
            new Team(teamLeads[0], juniors[0]),
            new Team(teamLeads[1], juniors[1]),
            new Team(teamLeads[2], juniors[2])
        };
        var director = new HrDirector();
        var harmony = director.CalculateHarmony(teams, teamLeadsWishlists, juniorsWishlists);
        var expectedHarmony = 1.6364f;
        Assert.That(Math.Round(harmony, 4), Is.EqualTo(Math.Round(expectedHarmony, 4)));
    }
}