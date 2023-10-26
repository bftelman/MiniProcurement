using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApplicationController : ControllerBase
    {
        protected new User User { get => Request.HttpContext.Items["User"] as User; }

    }
}
