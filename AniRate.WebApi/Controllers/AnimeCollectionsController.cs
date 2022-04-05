using AniRate.Application.AnimeCollections.Queries.GetAnimeCollectionById;
using AniRate.Application.AnimeCollections.Queries.GetAnimeCollections;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using AniRate.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AniRate.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AnimeCollectionsController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _dbContext;

        public AnimeCollectionsController(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<AnimeCollectionsListVM>> GetAll()
        {
            var query = new GetAnimeCollectionsQuery()
            {
                UserId = UserId
            };
            var collectionsVM = await Mediator.Send(query);
            return Ok(collectionsVM);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimeCollectionDetailsVM>> Get(string id)
        {
            var query = new GetAnimeCollectionByIdQuery()
            {
                UserId = UserId,
                Id = Guid.Parse(id)
            };

            var collectionDetailsVM = await Mediator.Send(query);
            return Ok(collectionDetailsVM);
        }

        //[HttpPost]
        //public async Task<ActionResult<Guid>> Create([FromBody] CreateAnimeCollectionDto createAnimeCollectionDto)
        //{
        //    var command = _mapper.Map<CreateAnimeCollectionCommand>(createAnimeCollectionDto);
        //    command.UserId = UserId;
        //    var collectionId = await Mediator.Send(command);
        //    return Ok(collectionId);
        //}

        //[HttpPut]
        //public async Task<ActionResult> Update([FromBody] UpdateAnimeCollectionDto updateAnimeCollectionDto)
        //{
        //    var command = _mapper.Map<UpdateAnimeCollectionCommand>(updateAnimeCollectionDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(string id)
        //{
        //    var command = new DeleteAnimeCollectionCommand
        //    {
        //        Id = id,
        //        UserId = UserId
        //    };
        //    await Mediator.Send(command);
        //    return NoContent();
        //}
    }
}
