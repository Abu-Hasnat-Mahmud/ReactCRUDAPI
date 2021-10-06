using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactCRUDAPI.Models;
using ReactCRUDAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDB _appDB;
        private readonly IUserService _service;
        public UserController(AppDB appDB, IUserService service)
        {
            _appDB = appDB;
            _service = service;
        }

        [HttpPost("LogIn")]
        public ActionResult LogIn(User user)
        {
            try
            {                
                var data = _service.Authenticate(user.UserName, user.Password); 
                if (data != null)
                {
                    return Ok(new { token= data });
                }
                else
                {
                    return new JsonResult(new { message = "Please input valid data!!" }) { StatusCode = StatusCodes.Status400BadRequest };
                }                
            }
            catch (Exception x)
            {
                return new JsonResult(new { message = x.Message }) { StatusCode = StatusCodes.Status400BadRequest };
            }
        }



    }
}
