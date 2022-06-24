using AniRate.Application.AnimeCollections.Commands.AddTitlesInCollections;
using AniRate.Application.AnimeCollections.Commands.CreateCollection;
using AniRate.Application.AnimeCollections.Commands.DeleteCollections;
using AniRate.Application.AnimeCollections.Commands.DeleteManyTitlesFromCollection;
using AniRate.Application.AnimeCollections.Commands.DeleteTitleFromManyCollections;
using AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails;
using AniRate.Application.AnimeCollections.Queries;
using AniRate.Application.AnimeCollections.Queries.GetCollectionDetails;
using AniRate.Application.AnimeCollections.Queries.GetCollections;
using AniRate.Application.AnimeCollections.Queries.SearchCollections;
using AniRate.Application.Common.Models;
using AniRate.Application.Interfaces;
using AniRate.WebApi.Models.AnimeCollectionsDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        /// получить все коллекции с пагинацией
        /// </summary>
        /// <returns>Returns CollectionsListVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedList<BriefCollectionVM>>> GetAll(int page, int size)
        {
            var query = new GetCollectionsQuery()
            {
                UserId = UserId,
                PageNumber = page,
                PageSize = size,
            };
            var collectionsVM = await Mediator.Send(query);
            return Ok(collectionsVM);
        }


        /// <summary>
        /// получить детали коллекции с пагинацией тайтлов
        /// </summary>
        /// <returns>Returns CollectionDetailsVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("CollectionDetails")]
        [Authorize]
        public async Task<ActionResult<CollectionDetailsVM>> Get(Guid id, int page, int size)
        {
            var query = new GetCollectionDetailsQuery()
            {
                UserId = UserId,
                Id = id,
                AnimeTitlesPageNumber = page,
                AnimeTitlesPageSize = size,
            };

            var collectionDetailsVM = await Mediator.Send(query);
            return Ok(collectionDetailsVM);
        }


        /// <summary>
        /// создать коллекцию со списком аниме (он может быть пустым)
        /// </summary>
        /// <returns>Guid id</returns>
        /// <response code="201">Success</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("Collection")]
        [Authorize]
        // Конечная точка контроллера - то, куда приходит запрос
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCollectionDto createCollectionDto)
        {
            // Формирование команды для слоя работы с сущностями, получив которую
            // слой сделает необходимые действия
            var command = _mapper.Map<CreateCollectionCommand>(createCollectionDto);
            command.UserId = UserId;
            // Отправка команды в слой работы с сущностями
            // И получение Id созданной коллекции по звершении работы слоя
            var collectionId = await Mediator.Send(command);
            // Отпрвка Id обратно клиентскому приложению
            return Ok(collectionId);
        }


        /// <summary>
        /// добавить тайтлы в коллекции
        /// </summary>
        /// <returns>NoContent</returns>
        /// <response code="204">Success</response>
        //[DisableCors]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("Titles")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] AddTitlesInCollectionsDto addTitlesInCollectionsDto)
        {
            var command = _mapper.Map<AddTitlesInCollectionsCommand>(addTitlesInCollectionsDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// изменить детали коллекции
        /// </summary>
        /// <returns>NoContent</returns>
        /// <response code="204">Success</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("ChangeDetails")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateCollectionDetailsDto updateCollectionDetailsDto)
        {
            var command = _mapper.Map<UpdateCollectionDetailsCommand>(updateCollectionDetailsDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// удалить коллекции
        /// </summary>
        /// <returns>NoContent</returns>
        /// <response code="204">Success</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("Collections")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteCollectionsDto deleteCollectionsDto)
        {
            var command = _mapper.Map<DeleteCollectionsCommand>(deleteCollectionsDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// удалить тайтлы из коллекции
        /// </summary>
        /// <returns>NoContent</returns>
        /// <response code="204">Success</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("ManyTitlesFromCollection")]
        [Authorize]
        public async Task<ActionResult> DeleteFromCollection([FromBody] DeleteManyTitlesFromCollectionDto deleteManyTitlesFromCollectionDto)
        {
            var command = _mapper.Map<DeleteManyTitlesFromCollectionCommand>(deleteManyTitlesFromCollectionDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }



        /// <summary>
        /// удалить тайтл из коллекций
        /// </summary>
        /// <returns>NoContent</returns>
        /// <response code="204">Success</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("TitleFromManyCollections")]
        [Authorize]
        public async Task<ActionResult> DeleteFromCollections([FromBody] DeleteTitleFromManyCollectionsDto deleteTitleFromManyCollectionsDto)
        {
            var command = _mapper.Map<DeleteTitleFromManyCollectionsCommand>(deleteTitleFromManyCollectionsDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Поиск коллекций
        /// </summary>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("SearchCollections")]
        [Authorize]
        public async Task<ActionResult<PaginatedList<BriefCollectionVM>>> SearchCollections(string searchString, int page, int size)
        {
            var query = new SearchCollectionsQuery()
            {
                UserId = UserId,
                PageNumber = page,
                PageSize = size,
                SearchString = searchString,
            };

            var collectionsVM = await Mediator.Send(query);
            return Ok(collectionsVM);
        }
    }
}
