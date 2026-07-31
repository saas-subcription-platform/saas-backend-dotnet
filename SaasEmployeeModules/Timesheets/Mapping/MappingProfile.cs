using AutoMapper;
using Timesheets.DTOs.Requests;
using Timesheets.DTOs.Responses;
using Timesheets.Entities;

namespace Timesheets.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TimesheetEntry, TimesheetEntryResponse>();

            CreateMap<Timesheet, TimesheetResponse>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<TimesheetRequest, Timesheet>();

            CreateMap<TimesheetEntryRequest, TimesheetEntry>();
        }
    }
}