using Hackathon.Entities;
using Nsu.HackathonProblem.Contracts;

namespace Hackathon;

public class HackathonMapper
{
    public HackathonEntity MapToEntity(Hackathon hackathon)
    {
        return new HackathonEntity()
        {
            Date = DateTime.Now
        };
    }

    public ParticipantEntity MapToEntity(Employee employee, HackathonEntity hackathonEntity, Role type)
    {
        return new ParticipantEntity()
        {
            EmployeeId = employee.Id,
            Name = employee.Name,
            HackathonId = hackathonEntity.Id,
            Type = type,
            Hackathon = hackathonEntity
        };
    }

    public PreferenceEntity MapToEntity(ParticipantEntity participant, ParticipantEntity desiredParticipant, int rank)
    {
        return new PreferenceEntity()
        {
            ParticipantId = participant.ParticipantId,
            PreferredParticipantId = desiredParticipant.ParticipantId,
            Rank = rank
        };
    }

    public TeamEntity MapToEntity(Team team, HackathonEntity hackathonEntity)
    {
        return new TeamEntity()
        {
            TeamLeadId = team.TeamLead.Id,
            JuniorId = team.Junior.Id,
            HackathonId = hackathonEntity.Id
        };
    }
}