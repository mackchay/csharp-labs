using Nsu.HackathonProblem.Contracts;

namespace Hackathon;

public static class WishlistGenerator
{
    public static List<Wishlist> Generate(List<Employee> employees, List<Employee> desiredEmployees)
    {
        List<Wishlist> wishlists = [];
        List<int> employeeIDs = [];
        employeeIDs.AddRange(desiredEmployees.Select(desiredEmployee => desiredEmployee.Id));

        foreach (var employee in employees)
        {
            Shuffle(employeeIDs);
            wishlists.Add(new Wishlist(employee.Id, employeeIDs.ToArray()));
        }

        return wishlists;
    } 
    
    private static void Shuffle<T>(IList<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}