using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public ActorController(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDto>> Get(int pageno = 1, int recordToTake = 2)
        {
            return await _applicationDbContext.Actors.OrderBy(a => a.Name).ProjectTo<ActorDto>(_mapper.ConfigurationProvider).Pagination(pageno, recordToTake).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> AddActor(ActorCreationDto actorCreationDto)
        {
            var actor = _mapper.Map<Actor>(actorCreationDto);
            await _applicationDbContext.Actors.AddAsync(actor);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateActor(ActorCreationDto actorCreationDto,int id)
        {
            var actor = await _applicationDbContext.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if(actor is null)
            {
                return NotFound();    
            }

            actor = _mapper.Map(actorCreationDto, actor); // check how this helps
            await _applicationDbContext.SaveChangesAsync();
            return Ok();

            // this is the example of updating with the connectd model
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateActorDisconnected(ActorCreationDto actorCreationDto, int id)
        {
            var actorExits = await  _applicationDbContext.Actors.AnyAsync(a => a.Id == id);

            if(!actorExits){
                return NotFound();
            }

            var actor = _mapper.Map<Actor>(actorCreationDto);
            actor.Id = id;

            _applicationDbContext.Update(actor);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();

            // this is the example of updating with disconnected model
        }
    }
}   