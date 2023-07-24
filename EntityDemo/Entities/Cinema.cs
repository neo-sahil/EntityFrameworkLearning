using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EntityDemo.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // [Precision(precision: 9, scale: 2)] // this is the way we can limit to decimal variable
        public Point Location { get; set; }
        public CinemaOffer CinemaOffer { get; set; } // this is a navigation property
        public HashSet<CinemaHall> CinemaHalls { get; set; }
        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(Cinema);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(Cinema);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }
        }
    }
}