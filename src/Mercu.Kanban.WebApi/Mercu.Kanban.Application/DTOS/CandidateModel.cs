namespace Mercu.Kanban.Application.DTOS
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class CandidateModel : AddCandidateRequest
    {
        public string JobTitle { get; set; }
    }
}
