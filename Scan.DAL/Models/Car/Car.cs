
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.DAL.Models.Car
{
    public class Car
    {
        [Key]
        public Guid CarId { get; set; }= Guid.NewGuid();

        [Required]
        public string UserId { get; set; }

        [Required, MaxLength(20)]
        public string PlateNumber { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        // Navigation
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public ICollection<Detection> Detections { get; set; }
    }
}
