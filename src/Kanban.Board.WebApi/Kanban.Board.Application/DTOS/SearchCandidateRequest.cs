namespace Kanban.Board.Application.DTOS
{
    public class SearchCandidateRequest
    {
        public string CandidateName { get; set; }
        public IList<int> InterviewerIds { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
