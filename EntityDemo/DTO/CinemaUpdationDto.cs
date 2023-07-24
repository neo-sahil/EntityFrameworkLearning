using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.DTO
{
    public class CinemaUpdationDto
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CinemaOfferUpdationDto CinemaOffer { get; set; }
        public CinemaHallUpdationDto[] CinemaHalls { get; set; }
    }
}

