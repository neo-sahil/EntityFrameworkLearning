using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.DTO
{
    public class MovieCreationDto
    {
        public string Title { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? PosterUrl { get; set; }
        public List<int> GenreIds { get; set; }
        public List<int> CinemaHallIds { get; set; }
        public List<MovieActorCreationDto> MovieActors { get; set; }
    }
}