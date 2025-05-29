using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.UnitTests.Features.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTests
{
    readonly Mock<ILeaveTypeRepository> _mockRepo;
    readonly IMapper _mapper;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateLeaveTypeCommand_AddsLeaveTypeToList()
    {
        CreateLeaveTypeCommandHandler handler = new CreateLeaveTypeCommandHandler(
            _mapper, _mockRepo.Object);

        var command = new CreateLeaveTypeCommand
        {
            Name = "New Test Type",
            DefaultDays = 12
        };

        var result = await handler.Handle(command, CancellationToken.None);

        var allLeaveTypes = await _mockRepo.Object.GetAllAsync();

        result.ShouldBeOfType<int>();
        allLeaveTypes.Count.ShouldBe(4);
        allLeaveTypes.Any(q => q.Name == "New Test Type" && q.DefaultDays == 12).ShouldBeTrue();
    }
}
