using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDemo.Enums;

namespace EntityDemo.DTO
{
    public class CinemaHallUpdationDto
    {
        public int Id { get; set; }
        public CinemaHallType CinemaHallType { get; set; }
        public decimal Cost { get; set; }
    }
}