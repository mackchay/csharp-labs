namespace Hackathon.Entities;

public class WishlistEntity
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }  // Человек, который составил список

    public int PreferredEmployeeId { get; set; }
    public EmployeeEntity PreferredEmployee { get; set; }  // Тот, кого он хочет видеть в команде

    public int Rank { get; set; }  
}