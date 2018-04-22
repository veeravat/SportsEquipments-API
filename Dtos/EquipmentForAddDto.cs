using System.ComponentModel.DataAnnotations;

namespace OOAD.Dtos
{
    public class EquipmentForAddDto
    {
        public int E_ID { get; set; }
        [Required] public string E_name { get; set; }
        public int E_amount { get; set; }
        public int E_total { get; set; }
        
    }
    public class EquipmentForRentDto
    {
        [Required] public int E_ID { get; set; }
        [Required] public int Rent_by { get; set; }
        
    }

        public class EquipmentForResvDto
    {
        [Required] public int E_ID { get; set; }
        [Required] public int Resv_by { get; set; }
        
    }
}