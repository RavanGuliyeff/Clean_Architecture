using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.LeaveAllocations.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(e => e.EmployeeId == userId
        && e.LeaveTypeId == leaveTypeId
        && e.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await _context.LeaveAllocations
            .Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        return await _context.LeaveAllocations
            .Where(e => e.EmployeeId == userId)
            .Include(e => e.LeaveType)
            .ToListAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
            .Include(e => e.LeaveType)
            .FirstOrDefaultAsync(e => e.EmployeeId == userId && e.LeaveTypeId == leaveTypeId);
    }
}