using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackathon.Entities;

public class EmployeeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }  // Уникальный ID в базе данных

    public int EmployeeId { get; set; }  // ID сотрудника, который может быть одинаковым для разных ролей (джун и тимлид)
    
    public string Name { get; set; }
    public Role Role { get; set; }  // Может быть "Junior" или "TeamLead"

    public ICollection<WishlistEntity> Wishlists { get; set; }
    public ICollection<TeamEntity> Teams { get; set; }
}