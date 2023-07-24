using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.DTO
{
    public class MovieFilterDto
    {
        public string? Title { get; set; }
        public int Genre { get; set; }
        public bool InCinemas { get; set; }
        public bool IsUpcomming { get; set; }
    }
}