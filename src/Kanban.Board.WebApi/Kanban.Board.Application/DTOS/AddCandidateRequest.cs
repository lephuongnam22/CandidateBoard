﻿namespace Kanban.Board.Application.DTOS
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public record AddCandidateRequest : CandidateModelBase
    {
        public IList<int> JobIds { get; set; }
        public IList<int> InterviewerIds { get; set; }
    }
}
