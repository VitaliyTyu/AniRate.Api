using AniRate.Application.AnimeCollections.Commands.AddTitlesInCollection;
using AniRate.Application.AnimeCollections.Commands.CreateCollection;
using AniRate.Application.AnimeCollections.Commands.DeleteCollections;
using AniRate.Application.AnimeCollections.Commands.DeleteTitlesFromCollection;
using AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails;
using AniRate.Application.AnimeCollections.Queries;
using AniRate.Application.AnimeCollections.Queries.GetCollectionDetails;
using AniRate.Application.AnimeCollections.Queries.GetCollections;
using AniRate.Application.AnimeCollections.Queries.GetCollectionsFromTitle;
using AniRate.Application.Common.Models;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using AniRate.WebApi.Models.AnimeCollectionsDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AniRate.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AnimeCollectionsController : ApiControllerBase
    {
        private readonly IMapper _mapper;

        public AnimeCollectionsController(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
        }


        /// <summary>
        /// получить все коллекции
        /// </summary>
        /// <returns>Returns CollectionsListVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedList<BriefCollectionVM>>> GetAll()
        {
            var query = new GetCollectionsQuery()
            {
                UserId = UserId
            };
            var collectionsVM = await Mediator.Send(query);
            return Ok(collectionsVM);
        }



        /// <summary>
        /// получить детали коллекции
        /// </summary>
        /// <returns>Returns CollectionDetailsVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("details")]
        [Authorize]
        public async Task<ActionResult<CollectionDetailsVM>> Get([FromQuery] GetCollectionDetailsQuery query)
        {
            //var query = new GetCollectionDetailsQuery()
            //{
            //    UserId = UserId,
            //    Id = id
            //};

            query.UserId = UserId;

            var collectionDetailsVM = await Mediator.Send(query);
            return Ok(collectionDetailsVM);
        }


        ///// <summary>
        ///// создать коллекцию со списком аниме
        ///// </summary>
        ///// <returns>Guid id</returns>
        ///// <response code="201">Success</response>
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[HttpPost("Collection")]
        //[Authorize]
        //public async Task<ActionResult<Guid>> Create([FromBody] CreateCollectionDto createCollectionDto)
        //{
        //    var command = _mapper.Map<CreateCollectionCommand>(createCollectionDto);
        //    command.UserId = UserId;
        //    var collectionId = await Mediator.Send(command);
        //    return Ok(collectionId);
        //}


        ///// <summary>
        ///// добавить аниме в коллекцию
        ///// </summary>
        ///// <returns>NoContent</returns>
        ///// <response code="204">Success</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[HttpPut("Titles")]
        ////[Authorize]
        //public async Task<ActionResult> Add([FromBody] AddTitlesInCollectionDto addTitlesInCollectionDto)
        //{
        //    var command = _mapper.Map<AddTitlesInCollectionCommand>(addTitlesInCollectionDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}


        ///// <summary>
        ///// изменить описание/имя коллекции
        ///// </summary>
        ///// <returns>NoContent</returns>
        ///// <response code="204">Success</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[HttpPut("Details")]
        ////[Authorize]
        //public async Task<ActionResult> Update([FromBody] UpdateCollectionDetailsDto updateCollectionDetailsDto)
        //{
        //    var command = _mapper.Map<UpdateCollectionDetailsCommand>(updateCollectionDetailsDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}


        ///// <summary>
        ///// удалить коллекции
        ///// </summary>
        ///// <returns>NoContent</returns>
        ///// <response code="204">Success</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[HttpDelete("Collections")]
        ////[Authorize]
        //public async Task<ActionResult> Delete([FromBody] DeleteCollectionsDto deleteCollectionsDto)
        //{
        //    var command = _mapper.Map<DeleteCollectionsCommand>(deleteCollectionsDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}


        ///// <summary>
        ///// удалить аниме из коллекции
        ///// </summary>
        ///// <returns>NoContent</returns>
        ///// <response code="204">Success</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[HttpDelete("TitlesFromCollection")]
        ////[Authorize]
        //public async Task<ActionResult> DeleteFromCollection([FromBody] DeleteTitlesFromCollectionDto deleteTitlesFromCollectionDto)
        //{
        //    var command = _mapper.Map<DeleteTitlesFromCollectionCommand>(deleteTitlesFromCollectionDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}







        ///// <summary>
        ///// получить коллекции определенного тайтла
        ///// </summary>
        ///// <returns>Returns CollectionDetailsVM</returns>
        ///// <response code="200">Success</response>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpGet("CollectionsFromTitle/{id}")]
        ////[Authorize]
        //public async Task<ActionResult<CollectionDetailsVM>> GetCollectionsFromTitle(Guid id)
        //{
        //    var query = new GetCollectionsFromTitleQuery()
        //    {
        //        Id = id,
        //        UserId = UserId,
        //    };
        //    var collectionDetailsVM = await Mediator.Send(query);

        //    return Ok(collectionDetailsVM);
        //}
    }
}
