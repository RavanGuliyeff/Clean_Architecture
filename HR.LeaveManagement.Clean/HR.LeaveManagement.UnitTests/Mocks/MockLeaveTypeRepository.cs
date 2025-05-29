using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
    {
        List<LeaveType> leaveTypes = new List<LeaveType>()
        {
            new LeaveType()
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new LeaveType()
            {
                Id = 2,
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new LeaveType()
            {
                Id = 3,
                DefaultDays = 15,
                Name = "Test Maternity"
            }
        };


        Mock<ILeaveTypeRepository> mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveTypes);

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            });


        mockRepo.Setup(r => r.IsLeaveTypeUniqueAsync(It.IsAny<string>()))
        .ReturnsAsync(true);


        return mockRepo;
    }
}
