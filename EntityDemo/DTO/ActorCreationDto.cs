using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.DTO
{
    public class ActorCreationDto
    {
       public string Name { get; set; }
        public string? Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}