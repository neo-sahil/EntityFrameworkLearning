using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.DTO
{
    public class GenreAddDto
    {
        [Required]
        public string Name { get; set; }
    }
}