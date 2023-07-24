using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityDemo.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        // [Unicode(false)] // this is make this varchar insted of varchar
        public string PosterUrl { get; set; }
        public List<Genre> Genres { get; set; }
        public List<CinemaHall> CinemaHall { get; set; }
        public List<MovieActor> MovieActors { get; set; }
    }
}