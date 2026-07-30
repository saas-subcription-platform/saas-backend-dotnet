using AutoMapper;
using Timesheets.DTOs.Responses;
using Timesheets.Repositories.Interfaces;
using Timesheets.Services.Interfaces;

public class TimesheetService : ITimesheetService
{
    private readonly ITimesheetRepository _repository;
    private readonly IMapper _mapper;

    public TimesheetService(
        ITimesheetRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TimesheetResponse>> GetAllByEmployeeIdAsync(int employeeId)
    {
        var timesheets = await _repository.GetAllByEmployeeIdAsync(employeeId);

        return _mapper.Map<IEnumerable<TimesheetResponse>>(timesheets);
    }
}