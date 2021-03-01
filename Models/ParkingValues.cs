using System.ComponentModel.DataAnnotations;

namespace ParkingApp.Models
{
    public class ParkingValues
    {
        [Key]
        public int LicensePlate { get; set; }
        public string Name { get; set; }
    }
}