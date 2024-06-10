using Kanban.Board.Domain.Entities;

namespace Kanban.Board.Application.DTOS
{
    public class CandidateStatusModel
    {
        public CandidateStatus CandidateStatus { get; set; }

        public IList<CandidateModel> Candidates { get; set; }
    }
}
