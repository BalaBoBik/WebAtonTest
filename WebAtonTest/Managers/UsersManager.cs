using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAtonTest.Data;
using WebAtonTest.Requests;
using WebAtonTest.Responses;

namespace WebAtonTest.Managers
{
    public class UsersManager : IUsersManager
    {
        private readonly WebAtonTestDbContext _dbContext;
        public UsersManager(WebAtonTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> CreateUser(ExecutorDataRequest executorDataRequest, CreateUserRequest createUserRequest)
        {
            if (executorDataRequest.Admin)
            {
                var newUser = new User
                {

                    Login = createUserRequest.Login,
                    Password = createUserRequest.Password,
                    Name = createUserRequest.Name,
                    Gender = createUserRequest.Gender,
                    Birthday = createUserRequest.Birthday,
                    Admin = createUserRequest.Admin,
                    CreatedOn = DateTime.Now,
                    CreatedBy = executorDataRequest.Login,
                };
                User? userWithThisLogin;
                if (_dbContext.Users.ToList().Count > 0)
                    userWithThisLogin = _dbContext.Users.ToList().FirstOrDefault(x => x.Login == newUser.Login);
                else
                    userWithThisLogin = null;

                if (userWithThisLogin == null)
                {
                    _dbContext.Users.Add(newUser);
                    await _dbContext.SaveChangesAsync();
                    return newUser;
                }
                else throw new Exception($"Пользователь с логином |{createUserRequest.Login}| уже существует");
            }
            else throw new Exception($"Эта операция доступна только Админам");
        }
        public async Task<User> UpdateUserData(ExecutorDataRequest executorDataRequest, string UserLogin, string newName)
        {
            if ((executorDataRequest.Admin) || ((executorDataRequest.RevokedOn == null) && (executorDataRequest.Login == UserLogin)))
            {
                User? user = _dbContext.Users.First(x => x.Login == UserLogin);
                if (user != null)
                {
                    user.Name = newName;
                    user.ModifiedOn = DateTime.Now;
                    user.ModifiedBy = executorDataRequest.Login;
                    await _dbContext.SaveChangesAsync();
                    return user;
                }
                else throw new Exception($"Пользователя с логином |{UserLogin}| не существует");
            }
            else throw new Exception("Эта операция доступна только Админу или лично пользователю");
        }
        public async Task<User> UpdateUserData(ExecutorDataRequest executorDataRequest, string UserLogin, int newGender)
        {
            if ((executorDataRequest.Admin) || ((executorDataRequest.RevokedOn == null) && (executorDataRequest.Login == UserLogin)))
            {
                User? user = _dbContext.Users.First(x => x.Login == UserLogin);
                if (user != null)
                {
                    user.Gender = newGender;
                    user.ModifiedOn = DateTime.Now;
                    user.ModifiedBy = executorDataRequest.Login;
                    await _dbContext.SaveChangesAsync();
                    return user;
                }
                else throw new Exception($"Пользователя с логином |{UserLogin}| не существует");
            }
            else throw new Exception("Эта операция доступна только Админу или лично пользователю");
        }
        public async Task<User> UpdateUserData(ExecutorDataRequest executorDataRequest, string UserLogin, DateTime newBirthday)
        {
            if ((executorDataRequest.Admin) || ((executorDataRequest.RevokedOn == null) && (executorDataRequest.Login == UserLogin)))
            {
                User? user = _dbContext.Users.First(x => x.Login == UserLogin);
                if (user != null)
                {
                    user.Birthday = newBirthday;
                    user.ModifiedOn = DateTime.Now;
                    user.ModifiedBy = executorDataRequest.Login;
                    await _dbContext.SaveChangesAsync();
                    return user;
                }
                else throw new Exception($"Пользователя с логином |{UserLogin}| не существует");
            }
            else throw new Exception("Эта операция доступна только Админу или лично пользователю");

        }
        public async Task<User> UpdateUserPassword(ExecutorDataRequest executorDataRequest, string UserLogin, string newPassword)
        {
            if ((executorDataRequest.Admin) || ((executorDataRequest.RevokedOn == null) && (executorDataRequest.Login == UserLogin)))
            {
                User? user = _dbContext.Users.First(x => x.Login == UserLogin);
                if (user != null)
                {
                    user.Password = newPassword;
                    user.ModifiedOn = DateTime.Now;
                    user.ModifiedBy = executorDataRequest.Login;
                    await _dbContext.SaveChangesAsync();
                    return user;
                }
                else throw new Exception($"Пользователя с логином |{UserLogin}| не существует");
            }
            else throw new Exception("Эта операция доступна только Админу или лично пользователю");

        }
        public async Task<User> UpdateUserLogin(ExecutorDataRequest executorDataRequest, string UserLogin, string newLogin)
        {
            if ((executorDataRequest.Admin) || ((executorDataRequest.RevokedOn == null) && (executorDataRequest.Login == UserLogin)))
            {
                User? user = _dbContext.Users.First(x => x.Login == UserLogin);
                if (user != null)
                {
                    user.Login = newLogin;
                    user.ModifiedOn = DateTime.Now;
                    user.ModifiedBy = executorDataRequest.Login;
                    await _dbContext.SaveChangesAsync();
                    return user;
                }
                else throw new Exception($"Пользователя с логином |{UserLogin}| не существует");
            }
            else throw new Exception("Эта операция доступна только Админу или лично пользователю");

        }
        public async Task<List<User>> GetUsers(ExecutorDataRequest executorDataRequest)
        {
            if (executorDataRequest.Admin)
            {
                var users = await _dbContext.Users.ToListAsync();

                users = users.FindAll(x => x.RevokedOn == null);

                users.OrderBy(x => x.CreatedBy);

                return users;
            }
            else throw new Exception("Доступно только Админам");
        }
        public async Task<GetUserByLoginResponse> GetUserByLogin(ExecutorDataRequest executorDataRequest, string login)
        {
            if (executorDataRequest.Admin)
            {
                User user = _dbContext.Users.FirstOrDefault(x => x.Login == login);
                GetUserByLoginResponse response = new GetUserByLoginResponse()
                {
                    Guid = user.Guid,
                    Login = user.Login,
                    Password = user.Password,
                    Name = user.Name,
                    Gender = user.Gender,
                    Birthday = user.Birthday,
                    Active = (user.RevokedOn == null)
                };
                return response;
            }
            else throw new Exception("Доступно только Админам");
        }
        public async Task<User> GetUserByLoginAndPassword(ExecutorDataRequest executorDataRequest, string login, string password)
        {
            if ((executorDataRequest.Admin) || ((executorDataRequest.RevokedOn == null) && (executorDataRequest.Login == login)))
            {
                User? user = _dbContext.Users.First(x => (x.Login == login) && (x.Password == password));
                if (user != null)
                {
                    return user;
                }
                else throw new Exception($"Логин или пароль не совпадают");
            }
            else throw new Exception("Эта операция доступна только Админу или лично пользователю");

        }
        public async Task<List<User>> GetUsersOlderThen(ExecutorDataRequest executorDataRequest, int age)
        {
            if (executorDataRequest.Admin)
            {
                var users = await _dbContext.Users.ToListAsync();
                users = users.FindAll(x => x.Birthday.Value.AddYears(age) < DateTime.Now);
                return users;
            }
            else throw new Exception("Доступно только Админам");
        }
        public async Task<User> RevokeUser(ExecutorDataRequest executorDataRequest, string login)
        {
            if (executorDataRequest.Admin)
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
                user.RevokedOn = DateTime.Now;
                user.RevokedBy = executorDataRequest.Login;
                await _dbContext.SaveChangesAsync();
                return user;
            }
            else throw new Exception("Доступно только Админам");
        }
        public async Task<User> RestoreUser(ExecutorDataRequest executorDataRequest, string login)
        {
            if (executorDataRequest.Admin)
            {
                User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
                user.RevokedOn = null;
                user.RevokedBy = null;
                await _dbContext.SaveChangesAsync();
                return user;
            }
            else throw new Exception("Доступно только Админам");
        }
    }
}
