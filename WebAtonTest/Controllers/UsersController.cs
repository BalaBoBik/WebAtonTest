using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAtonTest.Data;
using WebAtonTest.Managers;
using WebAtonTest.Requests;
using WebAtonTest.Responses;

namespace WebAtonTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager _usersManager;
        public UsersController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }
        [HttpPost("Create")]
        public async Task<User> CreateUser([FromQuery] ExecutorDataRequest executorDataRequest,[FromQuery] CreateUserRequest createUserRequest)
        {
            return await _usersManager.CreateUser(executorDataRequest, createUserRequest);
        }
        [HttpPut("UpdateName/{UserLogin}/{newName}")]
        public async Task<User> UpdateUserData(ExecutorDataRequest executorDataRequest, string UserLogin, string newName)
        {
            return await _usersManager.UpdateUserData(executorDataRequest, UserLogin, newName);
        }
        [HttpPut("UpdateGender/{UserLogin}/{newGender:int}")]
        public async Task<User> UpdateUserData([FromQuery]ExecutorDataRequest executorDataRequest, [FromRoute]string UserLogin, [FromRoute]int newGender)
        {
            return await _usersManager.UpdateUserData(executorDataRequest, UserLogin, newGender);
        }
        [HttpPut("UpdateBirthday/{UserLogin}/{newBirthday:datetime}")]
        public async Task<User> UpdateUserData([FromQuery]ExecutorDataRequest executorDataRequest,[FromRoute] string UserLogin, [FromRoute]DateTime newBirthday)
        {
            return await _usersManager.UpdateUserData(executorDataRequest, UserLogin, newBirthday);
        }
        [HttpPut("UpdatePassword/{UserLogin}/{newPassword}")]
        public async Task<User> UpdateUserPassword([FromQuery]ExecutorDataRequest executorDataRequest, [FromRoute]string UserLogin,[FromRoute] string newPassword)
        {
            return await _usersManager.UpdateUserPassword(executorDataRequest, UserLogin, newPassword);
        }
        [HttpPut("UpdateLogin/{UserLogin}/{newLogin}")]
        public async Task<User> UpdateUserLogin([FromQuery]ExecutorDataRequest executorDataRequest, [FromRoute]string UserLogin, [FromRoute]string newLogin)
        {
            return await _usersManager.UpdateUserLogin(executorDataRequest, UserLogin, newLogin);
        }
        [HttpGet("many/active")]
        public async Task<List<User>> GetUsers([FromQuery]ExecutorDataRequest executorDataRequest)
        {
            return await _usersManager.GetUsers(executorDataRequest);
        }
        [HttpGet("one/{login}")]
        public async Task<GetUserByLoginResponse> GetUserByLogin([FromQuery]ExecutorDataRequest executorDataRequest, [FromRoute] string login)
        {
            return await _usersManager.GetUserByLogin(executorDataRequest, login);
        }
        [HttpGet("one/{login}/{password}")]
        public async Task<User> GetUserByLoginAndPassword([FromQuery]ExecutorDataRequest executorDataRequest, [FromRoute]string login, [FromRoute]string password)
        {
            return await _usersManager.GetUserByLoginAndPassword(executorDataRequest, login, password);
        }
        [HttpGet("many/older/{age:int}")]
        public async Task<List<User>> GetUsersOlderThen([FromQuery]ExecutorDataRequest executorDataRequest,[FromRoute] int age)
        {
            return await _usersManager.GetUsersOlderThen(executorDataRequest, age);
        }
        [HttpDelete("delete/{login}")]
        public async Task<User> RevokeUser([FromQuery] ExecutorDataRequest executorDataRequest, [FromRoute]string login)
        {
            return await _usersManager.RevokeUser(executorDataRequest, login);
        }
        [HttpPut("restore/one/{login}")]
        public async Task<User> RestoreUser([FromQuery]ExecutorDataRequest executorDataRequest, [FromRoute] string login)
        {
            return await _usersManager.RestoreUser(executorDataRequest, login);
        }
    }
}
