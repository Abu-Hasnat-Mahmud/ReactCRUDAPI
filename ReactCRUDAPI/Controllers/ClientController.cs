using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactCRUDAPI.Helper;
using ReactCRUDAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly AppDB _appDB;
        public ClientController(AppDB appDB)
        {
            _appDB = appDB;
        }

        [HttpGet]
        public IActionResult Client()
        {
            var clients = _appDB.Client.ToList();
            return Ok(clients);
        }

        
        [HttpGet("{id}")]
        public IActionResult Client(int? id)
        {
            if (id == null) return BadRequest();

            var client = _appDB.Client.FirstOrDefault(c => c.ClientId == id);
            return Ok(client);
        }

        [HttpPost]
        public IActionResult Client(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (client.ClientId > 0) _appDB.Update(client); // for update
                    else _appDB.Add(client); // for create new

                    _appDB.SaveChanges();

                    return Ok(client);
                }
                else
                {
                    return new JsonResult(new { message = "Please input valid data!!" }) { StatusCode = StatusCodes.Status400BadRequest };
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }

        }



        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteClient(int? id)
        {
            try
            {
                if (id == null || id == 0) return BadRequest();

                var client = _appDB.Client.FirstOrDefault(c => c.ClientId == id);

                if (client != null)
                {
                    _appDB.Client.Remove(client);
                    _appDB.SaveChanges();
                    return Ok(client);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
