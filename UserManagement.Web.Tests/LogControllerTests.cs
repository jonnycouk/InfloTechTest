using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.ApiServices.Logs;
using UserManagement.Sdk.Model;
using UserManagement.Web.Mapper;
using UserManagement.Web.Models.Logs;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Data.Tests;

public class LogControllerTests
{
    [Fact]
    public void List_WhenServiceReturnsUsers_ModelMustContainLogs()
    {
        // Arrange
        var controller = CreateController();
        var logs = SetupLogs();

        // Act
        var result = controller.List(0, 10);

        // Assert
        result.Model
            .Should().BeOfType<LogListViewModel>()
            .Which.Items.Should().BeEquivalentTo(logs);
    }
    
    [Fact]
    public void List_WhenServiceReturnsNoLogs_ModelMustContainEmptyList()
    {
        // Arrange
        var controller = CreateController();
        var emptyList = new List<LogDto>();

        _logApiService
            .Setup(s => s.GetAll(0, 10))
            .Returns(emptyList);

        // Act
        var result = controller.List(0, 10);

        // Assert
        result.Model
            .Should().BeOfType<LogListViewModel>()
            .Which.Items.Should().BeEmpty();
    }
    
    private LogDto[] SetupLogs()
    {
        var logs = new[]
        {
            new LogDto
            {
                Id = 1,
                Summary =  "New User Created",
                Detail =  "User Created by API",
                CreatedUtc = new DateTime(2026, 09, 10),
                AffectedUser = new UserDto()
                {
                    Id = 1,
                    Forename = "Arya",
                    Surname = "Stark",
                    Email = "arya.stark@winterfellsolutions.com",
                    IsActive = true
                }
            }
        };
    
        _logApiService
            .Setup(s => s.GetAll(0, 10))
            .Returns(logs.ToList());

        return logs;
    }

    private readonly Mock<LogMapper> _logMapper = new();
    private readonly Mock<ILogApiService> _logApiService = new();
    
    private LogsController CreateController() => new(_logApiService.Object, _logMapper.Object);
}