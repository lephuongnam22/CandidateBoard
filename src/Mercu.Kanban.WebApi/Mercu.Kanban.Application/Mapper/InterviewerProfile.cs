using AutoMapper;
using Mercu.Kanban.Application.DTOS;
using Mercu.Kanban.Domain.Entities;

namespace Mercu.Kanban.Application.Mapper
{
    public class InterviewerProfile : Profile
    {
        public InterviewerProfile()
        {
            CreateMap<Interviewer, InterviewerModel>().ReverseMap();
        }
    }
}
