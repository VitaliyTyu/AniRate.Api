using AniRate.Application.AnimeTitles.Commands.AddCollectionsInTitle;
using AniRate.Application.AnimeTitles.Commands.CreateTitle;
using AniRate.Application.AnimeTitles.Commands.DeleteCollectionsFromTitle;
using AniRate.Application.AnimeTitles.Commands.DeleteTitles;
using AniRate.Application.AnimeTitles.Commands.MakeTitlesUnanonymous;
using AniRate.Application.AnimeTitles.Commands.UpdateTitleDetails;
using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.AnimeTitles.Queries.GetTitleDetails;
using AniRate.Application.AnimeTitles.Queries.GetTitles;
using AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection;
using AniRate.Application.Common.Models;
using AniRate.WebApi.Models.AnimeTitlesDtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AnimeTitlesController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        public AnimeTitlesController(IMapper mapper) => _mapper = mapper;

        /// <summary>
        /// Получение всех аниме
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
        /// Получение всех аниме в конкретной коллекции
        /// </summary>
        /// <returns>Returns TitlesListVM</returns>
        /// <response code="200">Success</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("TitlesFromCollection")]
        [Authorize]
        public async Task<ActionResult<TitlesListVM>> GetTitlesFromCollection([FromQuery] GetTitlesFromCollectionQuery query)
        {
            //var query = new GetTitlesFromCollectionQuery()
            //{
            //    CollectionId = id,
            //    UserId = UserId,
            //};

            query.UserId = UserId;

            var titles = await Mediator.Send(query);

            return Ok(titles);
        }


        ///// <summary>
        ///// изменить детали аниме
        ///// </summary>
        ///// <returns>NoContent</returns>
        ///// <response code="204">Success</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[HttpPut("Details")]
        //[Authorize]
        //public async Task<ActionResult> Update([FromBody] UpdateTitleDetailsDto updateTitleDetailsDto)
        //{
        //    var command = _mapper.Map<UpdateTitleDetailsCommand>(updateTitleDetailsDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}



        ///// <summary>
        ///// Получение всех анонимных аниме
        ///// </summary>
        ///// <returns>Returns TitlesListVM</returns>
        ///// <response code="200">Success</response>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpGet("anonymous")]
        //public async Task<ActionResult<PaginatedList<BriefTitleVM>>> GetAllAnonymous([FromQuery] GetTitlesQuery query)
        //{
        //    query.UserId = Guid.Empty;
        //    var titles = await Mediator.Send(query);

        //    return Ok(titles);
        //}

        ///// <summary>
        ///// Получение деталей анонимного аниме
        ///// </summary>
        ///// <returns>Returns TitleDetailsVM</returns>
        ///// <response code="200">Success</response>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpGet("anonymous/details/{id}")]
        //public async Task<ActionResult<TitleDetailsVM>> GetAnonymousDetails(Guid id)
        //{
        //    var query = new GetTitleDetailsQuery()
        //    {
        //        UserId = Guid.Empty,
        //        Id = id,
        //    };
        //    var titleDetailsVM = await Mediator.Send(query);

        //    return Ok(titleDetailsVM);
        //}



        ///// <summary>
        ///// привязать аниме тайтлы к пользователю
        ///// </summary>
        ///// <returns>Guid ids (новые ids)</returns>
        ///// <response code="201">Success</response>
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[HttpPost("unanonymous")]
        //[Authorize]
        //public async Task<ActionResult<List<Guid>>> Post([FromBody] MakeTitlesUnanonymousDto makeTitlesUnanonymousDto)
        //{
        //    var command = _mapper.Map<MakeTitlesUnanonymousCommand>(makeTitlesUnanonymousDto);
        //    command.UserId = UserId;
        //    var animeIds = await Mediator.Send(command);
        //    return Ok(animeIds);
        //}





        ///// <summary>
        ///// добавить коллекции к аниме
        ///// </summary>
        ///// <returns>NoContent</returns>
        ///// <response code="204">Success</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[HttpPut("Collections")]
        //[Authorize]
        //public async Task<ActionResult> Update([FromBody] AddCollectionsInTitleDto addCollectionsInTitleDto)
        //{
        //    var command = _mapper.Map<AddCollectionsInTitleCommand>(addCollectionsInTitleDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        ///// <summary>
        ///// удалить аниме тайтлы
        ///// </summary>
        ///// <returns>NoContent</returns>
        ///// <response code="204">Success</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[HttpDelete("Titles")]
        //[Authorize]
        //public async Task<ActionResult> Delete([FromBody] DeleteTitlesDto deleteTitlesDto)
        //{
        //    var command = _mapper.Map<DeleteTitlesCommand>(deleteTitlesDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        ///// <summary>
        ///// удалить коллекции из аниме
        ///// </summary>
        ///// <returns>NoContent</returns>
        ///// <response code="204">Success</response>
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[HttpDelete("CollectionsFromTitle")]
        //[Authorize]
        //public async Task<ActionResult> Delete([FromBody] DeleteCollectionsFromTitleDto
        //    deleteCollectionsFromTitleDto)
        //{
        //    var command = _mapper.Map<DeleteCollectionsFromTitleCommand>(deleteCollectionsFromTitleDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        /////// <summary>
        /////// создать аниме
        /////// </summary>
        /////// <returns>Guid id</returns>
        /////// <response code="201">Success</response>
        ////[ProducesResponseType(StatusCodes.Status201Created)]
        ////[HttpPost("Title")]
        //////[Authorize]
        ////public async Task<ActionResult<Guid>> Post([FromBody] CreateTitleDto createTitleDto)
        ////{
        ////    var command = _mapper.Map<CreateTitleCommand>(createTitleDto);
        ////    command.UserId = UserId;
        ////    var animeId = await Mediator.Send(command);
        ////    return Ok(animeId);
        ////}
    }
}
