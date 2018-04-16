using System.ComponentModel.DataAnnotations;

namespace OOAD.Models
{
    public class Equipments
    {
        [Key]
        public int E_ID { get; set; }
        public string E_name { get; set; }
        public int E_amount { get; set; }
        public int E_total { get; set; }
    }
    public class EquipmentsReserve
    {
        [Key]
        public int Resv_ID { get; set; }
        public int E_ID { get; set; }
        public string Resv_time { get; set; }
        public int Resv_by { get; set; }
    }
    public class EquipmentsRent
    {
        [Key]
        public int Rent_ID { get; set; }
        public int E_ID { get; set; }
        public string Rent_time { get; set; }
        public int Rent_by { get; set; }

    }
}