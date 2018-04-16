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
}