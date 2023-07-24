using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.DTO
{
    public class CinemaCreationDto
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CinemaOfferCreationDto CinemaOffer { get; set; }
        public CinemaHallCreationDto[] CinemaHalls { get; set; }
    }
}