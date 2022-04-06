using AniRate.Application.AnimeCollections.Commands.AddTitlesInCollection;
using AniRate.Application.AnimeCollections.Commands.CreateCollection;
using AniRate.Application.AnimeCollections.Commands.DeleteCollections;
using AniRate.Application.AnimeCollections.Commands.DeleteTitlesFromCollection;
using AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails;
using AniRate.Application.AnimeCollections.Queries.GetCollectionById;
using AniRate.Application.AnimeCollections.Queries.GetCollections;
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

        //получить все коллекции
        [HttpGet]
        public async Task<ActionResult<CollectionsListVM>> GetAll()
        {
            var query = new GetCollectionsQuery()
            {
                UserId = UserId
            };
            var collectionsVM = await Mediator.Send(query);
            return Ok(collectionsVM);
        }

        //получить коллекцию по id
        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionDetailsVM>> Get(Guid id)
        {
            var query = new GetCollectionByIdQuery()
            {
                UserId = UserId,
                Id = id
            };

            var collectionDetailsVM = await Mediator.Send(query);
            return Ok(collectionDetailsVM);
        }

        //создать коллекцию со списком аниме
        [HttpPost("CreatingCollection")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCollectionDto createCollectionDto)
        {
            var command = _mapper.Map<CreateCollectionCommand>(createCollectionDto);
            command.UserId = UserId;
            var collectionId = await Mediator.Send(command);
            return Ok(collectionId);
        }

        //добавить аниме в коллекцию
        [HttpPut("AddingTitles")]
        public async Task<ActionResult> Add([FromBody] AddTitlesInCollectionDto addTitlesInCollectionDto)
        {
            var command = _mapper.Map<AddTitlesInCollectionCommand>(addTitlesInCollectionDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        //изменить описание/имя коллекции
        [HttpPut("ChangingDetails")]
        public async Task<ActionResult> Update([FromBody] UpdateCollectionDetailsDto updateCollectionDetailsDto)
        {
            var command = _mapper.Map<UpdateCollectionDetailsCommand>(updateCollectionDetailsDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        //удалить коллекции
        [HttpDelete("DeletingCollections")]
        public async Task<ActionResult> Delete([FromBody] DeleteCollectionsDto deleteCollectionsDto)
        {
            var command = _mapper.Map<DeleteCollectionsCommand>(deleteCollectionsDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        //удалить аниме из коллекции
        [HttpDelete("DeletingAnime")]
        public async Task<ActionResult> DeleteFromCollection([FromBody] DeleteTitlesFromCollectionDto deleteTitlesFromCollectionDto)
        {
            var command = _mapper.Map<DeleteTitlesFromCollectionCommand>(deleteTitlesFromCollectionDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
