using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.Entities
{
    // [Table(name: "GenreTbl", Schema = "Movies")]  // this os specify tablename and schemea
    public class Genre
    {
        // public int GenreId { get; set; } this is convention
        public int Id { get; set; } // this is convention

        //[Key]
        //public int Identifire { get; set; } // this is by using data anotation or we can use fluent API

        // [MaxLength(150)]
        [StringLength(maximumLength:150)]
        // [Required] // this is make a nullable field not nullable
        // [Column("GenreName")] // this is to specify the column name
        public string Name { get; set; } // we can also use fluent api

        public bool IsDeleted { get; set; } = false;
        public HashSet<Movie>? Movies { get; set; }
    }
}