namespace UserManagementAPI.Services;
public class UserService : IUserService
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public IEnumerable<User> GetAllUsers()
    {
        return _users;
    }

    public User? GetUserById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public User CreateUser(CreateUserRequest request)
    {
        // Check if email already exists
        if (_users.Any(u => u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException("User with this email already exists");
        }

        var user = new User
        {
            Id = _nextId++,
            Name = request.Name,
            Email = request.Email,
            Department = request.Department,
            CreatedAt = DateTime.UtcNow
        };

        _users.Add(user);
        return user;
    }

    public User? UpdateUser(int id, UpdateUserRequest request)
    {
        var user = GetUserById(id);
        if (user == null) return null;

        // Check if email is being changed and if it conflicts with existing user
        if (!string.IsNullOrEmpty(request.Email) &&
            !request.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase) &&
            _users.Any(u => u.Id != id && u.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException("User with this email already exists");
        }

        if (!string.IsNullOrEmpty(request.Name))
            user.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Email))
            user.Email = request.Email;

        if (!string.IsNullOrEmpty(request.Department))
            user.Department = request.Department;

        user.UpdatedAt = DateTime.UtcNow;
        return user;
    }

    public bool DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user == null) return false;

        return _users.Remove(user);
    }
}
