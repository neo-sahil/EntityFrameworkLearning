using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.DTO
{
    public class CinemaOfferUpdationDto
    {
        public int Id { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        
        [Range(1,100)]
        public decimal DiscountPercentage { get; set; }
    }
}