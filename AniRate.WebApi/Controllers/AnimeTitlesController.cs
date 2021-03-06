using AniRate.Application.AnimeTitles.Comammands.AddAnimes;
using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.AnimeTitles.Queries.GetTitleDetails;
using AniRate.Application.AnimeTitles.Queries.GetTitles;
using AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection;
using AniRate.Application.AnimeTitles.Queries.SerchAnimes;
using AniRate.Application.Common.Models;
using AniRate.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniRate.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AnimeTitlesController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        public AnimeTitlesController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Получение всех аниме с пагинацией
        /// </summary>
        /// <returns>Returns TitlesListVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<BriefTitleVM>>> GetAll([FromQuery] GetTitlesQuery query)
        {
            var titles = await Mediator.Send(query);
            return Ok(titles);
        }


        /// <summary>
        /// Получение деталей определенного аниме
        /// </summary>
        /// <returns>Returns TitleDetailsVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TitleDetailsVM>> GetDetails(Guid id)
        {
            var query = new GetTitleDetailsQuery()
            {
                Id = id,
            };
            var titleDetailsVM = await Mediator.Send(query);
            return Ok(titleDetailsVM);
        }



        /// <summary>
        /// Получение всех аниме в конкретной коллекции с пагинацией аниме
        /// </summary>
        /// <returns>Returns TitlesListVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("TitlesFromCollection")]
        [Authorize]
        public async Task<ActionResult<PaginatedList<BriefTitleVM>>> GetTitlesFromCollection(Guid collectionId, int page, int size)
        {
            var query = new GetTitlesFromCollectionQuery()
            {
                CollectionId = collectionId,
                PageNumber = page,
                PageSize = size,
                UserId = UserId,
            };

            var titles = await Mediator.Send(query);
            return Ok(titles);
        }

        /// <summary>
        /// Поиск всех аниме
        /// </summary>
        /// <returns>Returns TitleDetailsVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("SearchTitles")]
        public async Task<ActionResult<PaginatedList<BriefTitleVM>>> SerchAnimes(string searchString, int page, int size)
        {
            var query = new SerchAnimesQuery()
            {
                SearchString = searchString,
                PageNumber = page,
                PageSize = size,
            };

            var titles = await Mediator.Send(query);
            return Ok(titles);
        }


        /// <summary>
        /// AddAnimes
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("AddAnimes")]
        public async Task Create([FromBody] AddAnimesDto addAnimesDto)
        {
            await Mediator.Send(_mapper.Map<AddAnimesCommand>(addAnimesDto));
        }
    }
}
