using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOAD.Data;


namespace OOAD.Controllers  
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UsersController : Controller 
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            // var users = await _context.Users.ToListAsync();
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
                               }).ToListAsync();
            return Ok(users);
            // throw new Exception("Test Exceotion");
            // return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public async Task<IActionResult> getUser([FromBody]string value)
        {
            
            Console.WriteLine(value);
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
                                   telephone = user.Telephon,
                                   rented = user.Rented,
                                   reseverd = user.Reserved,
                               }).FirstOrDefaultAsync(x => x.studentId == value);
            if (users != null)
            {
                return Ok(users);
            }else{
                return NoContent();
            }
        }
    }
}