using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class LogServiceTests
{
    [Fact]
    public void GetAll_WhenContextReturnsEntities_AssociatedUserCredentialsMustBeEmpty()
    {
        // Arrange
        var service = CreateService();
        SetupLogs();

        // Act
        var result = service.GetAll().FirstOrDefault();

        // Assert
        result.Should().NotBeNull();
        result!.AffectedUser.Should().NotBeNull();
        result.AffectedUser.PasswordHash.Should().Be(string.Empty);
        result.AffectedUser.PasswordSalt.Should().Be(string.Empty);
    }
    
    private IQueryable<Log> SetupLogs()
    {
        var logs = new[]
        {
            new Log
            {
                Id = 1,
                Summary =  "New User Created",
                Detail =  "User Created by API",
                CreatedUtc = new DateTime(2026, 09, 10),
                AffectedUser = new User()
                {
                    Id = 1,
                    Forename = "Arya",
                    Surname = "Stark",
                    JobTitle =  "Software Engineer",
                    Organisation = "Winterfell Solutions",
                    DateOfBirth =   new DateTime(1977, 09, 10),
                    Email = "arya.stark@winterfellsolutions.com",
                    IsActive = true,
                    PasswordHash = "",
                    PasswordSalt = ""
                }
            }
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<Log>())
            .Returns(logs);

        return logs;
    }

    private readonly Mock<IDataContext> _dataContext = new();
    private LogService CreateService() => new(_dataContext.Object);
}