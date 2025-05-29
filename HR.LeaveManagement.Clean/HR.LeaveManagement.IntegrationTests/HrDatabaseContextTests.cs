using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.IntegrationTests;

public class HrDatabaseContextTests
{
    HrDatabaseContext _hrDatabaseContext;
    public HrDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        
        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async Task Save_SetDateCreatedValue()
    {
        LeaveType leaveType = new LeaveType()
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _hrDatabaseContext.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task Save_SetDateModifiedValue()
    {
        LeaveType leaveType = new LeaveType()
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        await _hrDatabaseContext.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.Name = "Vacation Updated";

        _hrDatabaseContext.Update(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateModified.ShouldNotBeNull();
    }
}