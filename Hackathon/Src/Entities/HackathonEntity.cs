using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon.Entities;

public class HackathonEntity
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double HarmonyScore { get; set; }

    public ICollection<TeamEntity> Teams { get; set; }
}
