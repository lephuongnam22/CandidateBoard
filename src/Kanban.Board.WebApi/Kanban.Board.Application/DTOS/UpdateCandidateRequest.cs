﻿namespace Kanban.Board.Application.DTOS
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public record UpdateCandidateRequest : AddCandidateRequest
    {
        public string OldEmail { get; set; }
    }
}
