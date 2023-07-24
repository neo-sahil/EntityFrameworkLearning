using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityDemo.Context;
using EntityDemo.DTO;
using EntityDemo.Entities;
using EntityDemo.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EntityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CinemaController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CinemaController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<CinemaDto>> GetAllCinema()
        {
            return await _applicationDbContext.Cinemas.ProjectTo<CinemaDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult> GetCloser(double latitute, double longitute)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid:4326);
            var myLocation = geometryFactory.CreatePoint(new Coordinate(longitute,latitute));

            var maxDistanceMeters = 2000;

            var cinemas = await _applicationDbContext.Cinemas.OrderBy(c => c.Location.Distance(myLocation))
            .Where(c => c.Location.IsWithinDistance(myLocation, maxDistanceMeters))
            .Select(c => new {
                Name = c.Name,
                Distance = Math.Round(c.Location.Distance(myLocation))
            }).ToListAsync();

            return Ok(cinemas);
        }

        [HttpPost]
        public async Task<ActionResult> AddCinema(CinemaCreationDto cinemaCreationDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaCreationDto);
            await _applicationDbContext.Cinemas.AddAsync(cinema);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpadteCinema(CinemaUpdationDto cinemaUpdationDto, int id)
        {
            var cinema = await _applicationDbContext.Cinemas.Include(c => c.CinemaHalls).Include(c => c.CinemaOffer)
            .FirstOrDefaultAsync(c => c.Id == id);

            if(cinema is null){
                return NotFound();
            }

            cinema = _mapper.Map(cinemaUpdationDto, cinema); // automapper helps us in updating related data too
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetCinema(int id)
        {
             var cinema = await _applicationDbContext.Cinemas.Include(c => c.CinemaHalls).Include(c => c.CinemaOffer)
            .FirstOrDefaultAsync(c => c.Id == id);

            if(cinema is null){
                return NotFound();
            }

            cinema.Location = null;
            return Ok(cinema);
        }
    }
}