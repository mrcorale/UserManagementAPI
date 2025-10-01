namespace UserManagementAPI.Interface;
public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    User CreateUser(CreateUserRequest request);
    User? UpdateUser(int id, UpdateUserRequest request);
    bool DeleteUser(int id);
}
