using AutoMapper;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Domain.Entities;

namespace Mercu.Kanban.Application.Mapper
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<CandidateModel, Candidate>().ReverseMap();
            CreateMap<AddCandidateRequest, Candidate>().ReverseMap();
        }
    }
}
