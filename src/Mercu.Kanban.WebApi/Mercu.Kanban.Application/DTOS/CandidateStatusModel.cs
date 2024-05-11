﻿using Mercu.Kanban.Domain.Entities;

namespace Mercu.Kanban.Application.DTOS
{
    public class CandidateStatusModel
    {
        public CandidateStatus CandidateStatus { get; set; }

        public IEnumerable<CandidateModel> Candidates { get; set; }
    }
}
