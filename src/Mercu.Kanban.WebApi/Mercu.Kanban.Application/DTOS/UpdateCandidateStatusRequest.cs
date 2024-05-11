using Mercu.Kanban.Domain.Entities;

namespace Mercu.Kanban.Application.DTOS
{
    public record UpdateCandidateStatusRequest
    {
        public int Id { get; set; }
        public CandidateStatus CandidateStatus { get; set; }
    }
}
