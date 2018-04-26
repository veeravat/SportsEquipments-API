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
using Newtonsoft.Json.Linq;

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
            var amt = (equipmentForAddDto.E_total - existing.E_total) + existing.E_amount;
            // Console.WriteLine(amt);
            if (amt < 0)
            {
                return StatusCode(406);
            }
            existing.E_name = equipmentForAddDto.E_name;
            existing.E_total = equipmentForAddDto.E_total;
            existing.E_amount = amt;
            await _context.SaveChangesAsync();

            return Ok("updated successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipment()
        {
            var equipments = await _context.Equipments.ToListAsync();

            equipments.ForEach(getResv);

            return Ok(equipments);
        }
        public async void getResv(Equipments equipments)
        {

            var today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            // Console.WriteLine(today);
            var ResvEquipments = await _context.EquipmentsReserve
            .Where(x => x.E_ID == equipments.E_ID)
            .Where(x => x.Resv_status == "reserved")
            .Where(x => x.Resv_time < today)
            .ToListAsync();

            foreach (var item in ResvEquipments)
            {
                EquipmentsReserve resv = await _context.EquipmentsReserve.FindAsync(item.Resv_ID);
                Equipments Equipments = await _context.Equipments.FindAsync(item.E_ID);
                User user = await _context.Users.FindAsync(item.Resv_by);

                user.Reserved = 0;
                resv.Resv_status = "overdue";
                Equipments.E_amount += 1;
                Equipments.E_resv -= 1;
            }
            await _context.SaveChangesAsync();
        }
        [HttpPost("rent")]
        public async Task<IActionResult> Rent([FromBody]EquipmentForRentDto equipmentForRentDto)
        {
            var equipmentRent = new EquipmentsRent();
            equipmentRent.E_ID = equipmentForRentDto.E_ID;
            equipmentRent.Rent_by = equipmentForRentDto.Rent_by;
            equipmentRent.Rent_time = int.Parse(DateTime.Now.ToString("yyyyMMdd")); //yyyyMMddHHmmss
            equipmentRent.Rent_status = "inuse";
            await _context.EquipmentsRent.AddAsync(equipmentRent);

            User user = await _context.Users.FindAsync(equipmentForRentDto.Rent_by);
            user.Rented = 1;

            Equipments Equipments = await _context.Equipments.FindAsync(equipmentForRentDto.E_ID);
            Equipments.E_amount -= 1;
            Equipments.E_used += 1;
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpGet("reserv")]
        public async Task<IActionResult> getResv()
        {
            var reserved = await (from resv in _context.EquipmentsReserve
                                  join e in _context.Equipments on resv.E_ID equals e.E_ID
                                  join u in _context.Users on resv.Resv_by equals u.Id
                                  where resv.Resv_status == "reserved"
                                  select new
                                  {
                                      E_ID = e.E_ID,
                                      U_SID = u.StudentId,
                                      U_ID = u.Id,
                                      R_ID = resv.Resv_ID,
                                      U_Name = u.Firstname + " " + u.Lastname,
                                      RentTime = resv.Resv_time,
                                      E_name = e.E_name
                                  }
            ).ToListAsync();
            return Ok(reserved);
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return([FromBody]string Rent_ID)
        {
            int intID;
            int.TryParse(Rent_ID, out intID);
            EquipmentsRent RentItem = await _context.EquipmentsRent.FindAsync(intID);
            Equipments Equipments = await _context.Equipments.FindAsync(RentItem.E_ID);
            User user = await _context.Users.FindAsync(RentItem.Rent_by);
            var ReturnItem = new EquipmentsReturn();

            RentItem.Rent_status = "return";
            user.Rented = 0;
            Equipments.E_amount += 1;
            Equipments.E_used -= 1;
            ReturnItem.Rent_ID = intID;
            ReturnItem.Return_time = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

            await _context.EquipmentsReturn.AddAsync(ReturnItem);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPost("reserv")]
        public async Task<IActionResult> Reserv([FromBody]EquipmentForResvDto equipmentForResvDto)
        {
            var ResvItem = new EquipmentsReserve();
            ResvItem.E_ID = equipmentForResvDto.E_ID;
            ResvItem.Resv_by = equipmentForResvDto.Resv_by;
            ResvItem.Resv_time = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            ResvItem.Resv_status = "reserved";

            User user = await _context.Users.FindAsync(equipmentForResvDto.Resv_by);
            user.Reserved = 1;

            Equipments Equipments = await _context.Equipments.FindAsync(ResvItem.E_ID);
            Equipments.E_amount -= 1;
            Equipments.E_resv += 1;

            await _context.EquipmentsReserve.AddAsync(ResvItem);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPut("reserv")]
        public async Task<IActionResult> getReserv([FromBody]int resvid)
        {
            EquipmentsReserve ResvItem = await _context.EquipmentsReserve.FindAsync(resvid);
            User user = await _context.Users.FindAsync(ResvItem.Resv_by);
            Equipments Equipments = await _context.Equipments.FindAsync(ResvItem.E_ID);
            EquipmentForRentDto rentItem = new EquipmentForRentDto();

            user.Reserved = 0;
            Equipments.E_resv -= 1;
            Equipments.E_amount += 1;
            ResvItem.Resv_status = "rented";
            rentItem.E_ID = ResvItem.E_ID;
            rentItem.Rent_by = ResvItem.Resv_by;

            await _context.SaveChangesAsync();
            await Rent(rentItem);
            return StatusCode(201);
        }


        [HttpGet("inuse")]
        public async Task<IActionResult> getInUse()
        {
            var equipments = await (from rent in _context.EquipmentsRent
                                    join e in _context.Equipments on rent.E_ID equals e.E_ID
                                    join u in _context.Users on rent.Rent_by equals u.Id
                                    where rent.Rent_status == "inuse"
                                    select new
                                    {
                                        E_ID = e.E_ID,
                                        E_Name = e.E_name,
                                        U_ID = u.Id,
                                        U_Name = u.Firstname + " " + u.Lastname,
                                        RentTime = rent.Rent_time,
                                        R_ID = rent.Rent_ID,
                                        U_SID = u.StudentId
                                    }
            ).ToListAsync();
            return Ok(equipments);
        }
        [HttpGet("dashboard")]
        public IActionResult getDashboard()
        {
            dynamic json = new JObject();
            var today = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            json.equipments = _context.Equipments.Sum(x => x.E_amount);
            json.inuse = _context.EquipmentsRent.Where(x => x.Rent_status == "inuse").Count();
            json.resv = _context.EquipmentsReserve.Where(x => x.Resv_status == "reserved").Count();
            json.overdue = _context.EquipmentsRent.Where(x => x.Rent_status == "inuse")
            .Where(x => x.Rent_time < today)
            .Count();

            return Ok(json);
        }
    }
}