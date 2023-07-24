using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityDemo.Context;
using EntityDemo.DTO;
using EntityDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public MovieController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDto>> Get(int id)
        {
            var movies = await _applicationDbContext.Movies
            .Include(m => m.Genres.OrderBy(g => g.Name).Where(g => !g.Name.Contains("m")))
            .Include(m => m.CinemaHall)
                .ThenInclude(ch => ch.Cinema)
            .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
            .FirstOrDefaultAsync(m => m.Id == id);

            if(movies is null){
                return null;
            }

            var movieDto = _mapper.Map<MovieDto>(movies);

            movieDto.Cinemas = movieDto.Cinemas.DistinctBy(c => c.Id).ToList();

            return movieDto;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDto>> GetByAutoMapper(int id)
        {
            var moviesdto = await _applicationDbContext.Movies
            .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(m => m.Id == id);

            if(moviesdto is null){
                return null;
            }

            moviesdto.Cinemas = moviesdto.Cinemas.DistinctBy(c => c.Id).ToList();

            return moviesdto;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetSelectLoading(int id)
        {
            var movieDto = await _applicationDbContext.Movies.Select(m => new {
                Id = m.Id,
                Title = m.Title,
                Genres = m.Genres.Select(g => g.Name).OrderByDescending(n => n).ToList()
            }).FirstOrDefaultAsync(m => m.Id == id);

            if(movieDto is null)
            {
                return NotFound();           
            }

            return Ok(movieDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetExplicitLoading(int id)
        {
            var movie = await _applicationDbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if(movie is null)
            {
                return NotFound();           
            }

            // await _applicationDbContext.Entry(movie).Collection(p => p.Genres).LoadAsync();

            var genresCount = await _applicationDbContext.Entry(movie).Collection(p => p.Genres).Query().CountAsync();

            var movieDto = _mapper.Map<MovieDto>(movie);

            // return Ok(movieDto);

            return Ok(new {
                Id = movieDto.Id,
                Title = movieDto.Title,
                GenreCount = genresCount
            });

        }
        
        [HttpGet]
        public async Task<ActionResult> GetGroupedByCinema()
        {
            var groupedMovies = await _applicationDbContext.Movies.GroupBy(m => m.InCinemas).Select(g => new {
                InCinemas = g.Key,
                Count = g.Count(),
                Movies = g.ToList() 
            }).ToListAsync();

            return Ok(groupedMovies);

        }

        [HttpGet]
        public async Task<ActionResult> GetGroupedByGenres()
        {
            var groupedMovies = await _applicationDbContext.Movies.GroupBy(m => m.Genres.Count()).Select(g => new {
                Count = g.Key,
                Titles = g.Select(m => m.Title),
                Genre = g.Select(m => m.Genres).SelectMany(a => a).Select(ge => new {
                    Id = ge.Id,
                    Name = ge.Name
                }).Distinct()
            }).ToListAsync();

            return Ok(groupedMovies);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> FilterMovies([FromQuery] MovieFilterDto movieFilterDto)
        {
            var moviesQueryable = _applicationDbContext.Movies.AsQueryable();

            if(!string.IsNullOrEmpty(movieFilterDto.Title))
            {
                moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(movieFilterDto.Title));
            }

            if(movieFilterDto.InCinemas)
            {
                moviesQueryable = moviesQueryable.Where(m => m.InCinemas);
            }

            if(movieFilterDto.Genre != 0)
            {
                moviesQueryable = moviesQueryable.Where(m => m.Genres.Select(g => g.Id).Contains(movieFilterDto.Genre));
            }

            if(movieFilterDto.IsUpcomming)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > today);
            }

            var movies = await moviesQueryable.Include(m => m.Genres).ToListAsync();

            return _mapper.Map<List<MovieDto>>(movies);
        }

        [HttpPost]
        public async Task<ActionResult> AddMovie(MovieCreationDto movieCreationDto)
        {
            var movie = _mapper.Map<Movie>(movieCreationDto);

            movie.Genres.ForEach(g => _applicationDbContext.Entry(g).State = EntityState.Unchanged);
            movie.CinemaHall.ForEach(ch => _applicationDbContext.Entry(ch).State = EntityState.Unchanged);
            //this i have to check this has something todo with automatic third level normalization

            if(movie.MovieActors is not null){
                for(int i = 0; i < movie.MovieActors.Count; i++){
                    movie.MovieActors[i].Order = i+1;
                }
            }

            await _applicationDbContext.Movies.AddAsync(movie);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}