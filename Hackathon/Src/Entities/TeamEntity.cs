using Castle.Components.DictionaryAdapter;

namespace Hackathon.Entities;

public class TeamEntity
{
    public int TeamId { get; set; }
    
    public int TeamLeadId { get; set; }
    public ParticipantEntity TeamLead { get; set; }
    
    public int JuniorId { get; set; }
    public ParticipantEntity Junior { get; set; }
    
    public int HackathonId { get; set; }
    public HackathonEntity Hackathon { get; set; }
}
