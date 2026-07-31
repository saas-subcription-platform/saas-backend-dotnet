using AutoMapper;
using Timesheets.Common.Enums;
using Timesheets.DTOs.Requests;
using Timesheets.DTOs.Responses;
using Timesheets.Entities;
using Timesheets.Repositories.Implementations;
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

    public async Task<TimesheetResponse?> GetByIdAsync(int id, int employeeId)
    {
        var timesheet = await _repository.GetByIdAsync(id);

        if (timesheet == null || timesheet.EmployeeId != employeeId)
            return null;

        return _mapper.Map<TimesheetResponse>(timesheet);
    }

    public async Task<TimesheetResponse> CreateAsync(
    TimesheetRequest request,
    int employeeId)
    {
        var timesheet = _mapper.Map<Timesheet>(request);

        timesheet.EmployeeId = employeeId;
        timesheet.Status = TimesheetStatus.Draft;
        timesheet.TotalHours = request.Entries.Sum(e => e.Hours);
        timesheet.CreatedAt = DateTime.UtcNow;
        timesheet.UpdatedAt = DateTime.UtcNow;

        await _repository.AddAsync(timesheet);

        await _repository.SaveChangesAsync();

        return _mapper.Map<TimesheetResponse>(timesheet);
    }

    public async Task<TimesheetResponse?> UpdateAsync(
    int id,
    TimesheetRequest request,
    int employeeId)
    {
        var timesheet = await _repository.GetByIdAsync(id);

        if (timesheet == null || timesheet.EmployeeId != employeeId)
            return null;

        timesheet.WeekStartDate = request.WeekStartDate;
        timesheet.WeekEndDate = request.WeekEndDate;
        timesheet.TotalHours = request.Entries.Sum(e => e.Hours);
        timesheet.UpdatedAt = DateTime.UtcNow;

        timesheet.Entries.Clear();

        foreach (var entry in request.Entries)
        {
            timesheet.Entries.Add(new TimesheetEntry
            {
                Day = entry.Day,
                ProjectName = entry.ProjectName,
                Task = entry.Task,
                Hours = entry.Hours,
                Description = entry.Description
            });
        }

        _repository.Update(timesheet);

        await _repository.SaveChangesAsync();

        return _mapper.Map<TimesheetResponse>(timesheet);
    }

    public async Task<TimesheetResponse?> SubmitAsync(int id, int employeeId)
    {
        var timesheet = await _repository.GetByIdAsync(id);

        if (timesheet == null || timesheet.EmployeeId != employeeId)
            return null;

        if (timesheet.Status == TimesheetStatus.Submitted)
            return _mapper.Map<TimesheetResponse>(timesheet);

        timesheet.Status = TimesheetStatus.Submitted;
        timesheet.SubmittedOn = DateTime.UtcNow;
        timesheet.UpdatedAt = DateTime.UtcNow;

        _repository.Update(timesheet);
        await _repository.SaveChangesAsync();

        return _mapper.Map<TimesheetResponse>(timesheet);
    }
}