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
        [HttpPost("new")]
        public async Task<IActionResult> addEquipment([FromBody]EquipmentForAddDto equipmentForAddDto)
        {
            var exist = await _context.Equipments.FirstOrDefaultAsync(x => x.E_name == equipmentForAddDto.E_name);

            if (exist != null)
            {
                return BadRequest();
            }

            var equipment = new Equipments();
            equipment.E_name = equipmentForAddDto.E_name;
            equipment.E_total = 0;
            equipment.E_amount = 0;
            await _context.Equipments.AddAsync(equipment);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPost("update")]
        public async Task<IActionResult> updateEquipment([FromBody]EquipmentForAddDto equipmentForAddDto)
        {
            Equipments existing = await _context.Equipments.FindAsync(equipmentForAddDto.E_ID);
            existing.E_name = equipmentForAddDto.E_name;
            existing.E_total = equipmentForAddDto.E_total;
            await _context.SaveChangesAsync();

            return Ok("updated successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var equipments = await _context.Equipments.ToListAsync();
            return Ok(equipments);
        }
    }
}