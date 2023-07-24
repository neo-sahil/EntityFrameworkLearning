using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDemo.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityDemo.Entities.Copnfiguration
{
    public class CinemaHallConfig : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            builder.Property(p => p.Cost).HasPrecision(precision: 9, scale: 2);
            builder.Property(p => p.CinemaHallType).HasDefaultValue(CinemaHallType.TowDimension);
        }
    }
}