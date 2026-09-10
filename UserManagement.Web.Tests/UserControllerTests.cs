using UserManagement.ApiServices.Logs;
using UserManagement.ApiServices.Users;
using UserManagement.Sdk.Model;
using UserManagement.Web.Mapper;
using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Data.Tests;

public class UserControllerTests
{
    [Fact]
    public void List_WhenServiceReturnsUsers_ModelMustContainUsers()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List(null);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().BeEquivalentTo(users);
    }

    [Fact]
    public void List_WhenServiceReturnsUsers_ModelMustContainActiveUsers()
    {
        // Arrange
        var controller = CreateController();
        SetupUsers();

        // Act
        var result = controller.List(true);

        // Assert
        result.Model.Should().BeOfType<UserListViewModel>().Which.Items.Should().OnlyContain(c => c.IsActive);
    }

    [Fact]
    public void List_WhenServiceReturnsUsers_ModelMustContainNonActiveUsers()
    {
        // Arrange
        var controller = CreateController();
        SetupUsers();

        // Act
        var result = controller.List(false);
        
        //Assert
        result.Model.Should().BeOfType<UserListViewModel>().Which.Items.Should().OnlyContain(c => !c.IsActive);
    }

    private UserDto[] SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
    {
        var users = new[]
        {
            new UserDto
            {
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive
            }
        };

        _userApiService
            .Setup(s => s.GetAll(""))
            .Returns(users);

        return users;
    }

    private readonly Mock<IUserApiService> _userApiService = new();
    private readonly Mock<UserMapper> _userMapper = new();
    private readonly Mock<LogMapper> _logMapper = new();
    private readonly Mock<ILogApiService> _logApiService = new();
    
    
    private UsersController CreateController() => new(_userApiService.Object, _logApiService.Object, _logMapper.Object, _userMapper.Object);
}
