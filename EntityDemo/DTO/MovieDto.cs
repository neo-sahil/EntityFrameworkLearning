using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.DTO
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public ICollection<CinemaDto> Cinemas { get; set; }
        public ICollection<ActorDto> Actors { get; set; }
    }
}