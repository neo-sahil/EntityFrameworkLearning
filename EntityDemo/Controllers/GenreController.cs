using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EntityDemo.Context;
using EntityDemo.DTO;
using EntityDemo.Entities;
using EntityDemo.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GenreController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> GetGenres()
        {
            // return await _applicationDbContext.Genres.AsNoTracking().ToListAsync();
            // return await _applicationDbContext.Genres.AsTracking().ToListAsync();
            // return await _applicationDbContext.Genres.Where(g => !g.IsDeleted).ToListAsync();

            // Note if i dot'nt want to apply the "Where(g => !g.IsDeleted)" every time then i can give it in genre entity confiurations as a Query Filters
            return await _applicationDbContext.Genres.ToListAsync();
        }

        [HttpGet]
        public async Task<Genre> GetFirst()
        {
            return await _applicationDbContext.Genres.FirstOrDefaultAsync(g => g.Name.Contains("k"));
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> FilterGenre(string name)
        {
            return await _applicationDbContext.Genres.Where(g => g.Name.Contains(name)).ToListAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> GetByPagination(int pageno = 1, int recordToTake = 2)
        {
            return await _applicationDbContext.Genres.OrderBy(g => g.Name).Pagination(pageno, recordToTake).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddGenre(GenreAddDto genreAddDto)
        {
            var genre = _mapper.Map<Genre>(genreAddDto);
            var status1 = _applicationDbContext.Entry(genre).State; // this will give detached
            await _applicationDbContext.Genres.AddAsync(genre); // marking the status of genre to added
            var status2 = _applicationDbContext.Entry(genre).State; // this will give added
            await _applicationDbContext.SaveChangesAsync();
            var status3 = _applicationDbContext.Entry(genre).State; // this will give attched
            return Ok(genre.Id);
        }

        [HttpPost]
        public async Task<ActionResult> AddMultipleGenre(GenreAddDto[] genreAddDto)
        {
            var genre = _mapper.Map<Genre[]>(genreAddDto);
            await _applicationDbContext.Genres.AddRangeAsync(genre); // marking the status of genre to added
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        } 

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var genre = await _applicationDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if(genre is null){
                return NotFound();
            }

            _applicationDbContext.Remove(genre);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> SoftDeleteGenre(int id)
        {
            var genre = await _applicationDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);

            if(genre is null){
                return NotFound();
            }

            genre.IsDeleted = true;
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> RestoreGenre(int id)
        {
            var genre = await _applicationDbContext.Genres.IgnoreQueryFilters().FirstOrDefaultAsync(g => g.Id == id);

            if(genre is null){
                return NotFound();
            }

            genre.IsDeleted = false;
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}