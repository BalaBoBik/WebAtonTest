using Microsoft.AspNetCore.Identity.Data;

namespace WebAtonTest.Requests
{
    public class ExecutorDataRequest
    {
        public Guid Guid {  get; set; } 
        public bool Admin { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? RevokedOn { get; set; }
    }
}
