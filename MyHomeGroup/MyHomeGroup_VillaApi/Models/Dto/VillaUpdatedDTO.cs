using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyHomeGroup_VillaApi.Models.Dto
{
    public class VillaUpdatedDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public Amenties Amenties { get; set; }

        //public int AmentiesId { get; set; }

        public string Details { get; set; }

        public double PriceRangeStarts { get; set; }

        public int Sqft { get; set; }

        public string ImageUrl { get; set; }

        public int Occupancy { get; set; }
    }
}
