using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.AnimeTitles.Queries.GetTitleDetails;
using AniRate.Application.AnimeTitles.Queries.GetTitles;
using AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection;
using AniRate.Application.Common.Models;
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
        //[Authorize]
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
        //[Authorize]
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
        public async Task<ActionResult<TitlesListVM>> GetTitlesFromCollection([FromQuery] GetTitlesFromCollectionQuery query)
        {
            query.UserId = UserId;
            var titles = await Mediator.Send(query);
            return Ok(titles);
        }
    }
}
