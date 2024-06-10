using Kanban.Board.Domain.Entities;

namespace Kanban.Board.Application.DTOS
{
    public record UpdateCandidateStatusRequest
    {
        public int Id { get; set; }
        public CandidateStatus CandidateStatus { get; set; }
    }
}
