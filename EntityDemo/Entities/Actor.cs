using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityDemo.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        private string _name;
        public string Name { 
            get{
                return _name;
            } 
            set{
                _name = string.Join(' ', value.Split(' ').Select(n => n[0].ToString().ToUpper() + n.Substring(1).ToLower()).ToArray());
            } 
        }
        public string? Biography { get; set; }
        // [Column(TypeName = "Date")]  // this is to give a specific tyoe to a column
        public DateTime? DateOfBirth { get; set; }
        public HashSet<MovieActor>? MovieActors { get; set; }
    }
}