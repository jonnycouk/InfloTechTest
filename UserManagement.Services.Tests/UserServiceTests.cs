using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class UserServiceTests
{
    [Fact]
    public void GetAll_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange
        var service = CreateService();
        var users = SetupUsers();

        // Act
        var result = service.GetAll();

        // Assert
        result.Should().BeSameAs(users);
    }
    
    [Fact]
    public void GetById_WhenUserExists_MustReturnCorrectUser()
    {
        // Arrange
        var service = CreateService();
        var users = SetupUsers();
        var expectedUser = users.First();
        
        _dataContext
            .Setup(s => s.GetById<User>(expectedUser.Id))
            .Returns(expectedUser);

        // Act
        var result = service.GetById(expectedUser.Id);

        // Assert
        result.Should().BeEquivalentTo(expectedUser);
    }
    
    
    private IQueryable<User> SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
    {
        var users = new[]
        {
            new User
            {
                Id = 1,
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive
            }
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<User>())
            .Returns(users);

        return users;
    }

    private readonly Mock<IDataContext> _dataContext = new();
    private UserService CreateService() => new(_dataContext.Object);
}
