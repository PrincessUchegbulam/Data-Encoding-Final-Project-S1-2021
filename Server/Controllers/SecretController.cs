using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class SecretController : Controller
    {
        // secret controller with secret message 
        [Authorize] 
        public string Index()
        {
            return "secret message";
        }
    }
}
