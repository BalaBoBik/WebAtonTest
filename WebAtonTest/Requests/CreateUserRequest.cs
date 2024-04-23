using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAtonTest.Requests
{
    public class CreateUserRequest
    {
        public string Login { get;set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime Birthday { get; set; }
        public bool Admin { get; set; }
    }
}
