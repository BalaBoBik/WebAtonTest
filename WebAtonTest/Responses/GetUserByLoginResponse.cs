using System.ComponentModel.DataAnnotations;

namespace WebAtonTest.Responses
{
    public class GetUserByLoginResponse
    {
        public Guid Guid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool Active { get; set; }
    }
}
