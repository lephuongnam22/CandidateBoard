namespace Mercu.Kanban.Application.DTOS
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public record CandidateModel : CandidateModelBase
    {
        public IList<JobModel> JobModels { get; set; }
        public IList<InterviewerModel> InterviewerModels { get; set; }
    }
}
