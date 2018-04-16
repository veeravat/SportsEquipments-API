using Microsoft.AspNetCore.Mvc;
using OOAD.Data;
using OOAD.Dtos;
using OOAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace OOAD.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]

    public class EquipmentController : Controller
    {
        private readonly DataContext _context;
        public EquipmentController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> getUser([FromBody]string value)
        {

            var users = await (from user in _context.Users
                               select new
                               {
                                   id = user.Id,
                                   username = user.Username,
                                   studentId = user.StudentId,
                                   firstname = user.Firstname,
                                   lastname = user.Lastname,
                                   role = user.Role,
                                   email = user.Email,
                                   telephone = user.Telephon
                               }).FirstOrDefaultAsync(x => x.studentId == value);
            return Ok();
        }

    }
}