using AniRate.Application.AnimeTitles.Commands.AddCollectionsInTitle;
using AniRate.Application.AnimeTitles.Commands.CreateTitle;
using AniRate.Application.AnimeTitles.Commands.DeleteCollectionsFromTitle;
using AniRate.Application.AnimeTitles.Commands.DeleteTitles;
using AniRate.Application.AnimeTitles.Commands.UpdateTitleDetails;
using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.AnimeTitles.Queries.GetTitleDetails;
using AniRate.Application.AnimeTitles.Queries.GetTitles;
using AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection;
using AniRate.WebApi.Models.AnimeTitlesDtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AnimeTitlesController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        public AnimeTitlesController(IMapper mapper) => _mapper = mapper;


        //Получение всех аниме
        [HttpGet]
        public async Task<ActionResult<TitlesListVM>> GetAll()
        {
            var query = new GetTitlesQuery()
            {
                UserId = UserId,
            };
            var animeTitlesVM = await Mediator.Send(query);

            return Ok(animeTitlesVM);
        }

        //Получение всех аниме в конкретной коллекции
        [HttpGet("TitlesFromCollection/{id}")]
        public async Task<ActionResult<TitlesListVM>> GetTitlesFromCollection(Guid id)
        {
            var query = new GetTitlesFromCollectionQuery()
            {
                Id = id,
                UserId = UserId,
            };
            var animeTitlesVM = await Mediator.Send(query);

            return Ok(animeTitlesVM);
        }

        //Получение деталей определенного аниме
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<TitleDetailsVM>> GetDetails(Guid id)
        {
            var query = new GetTitleDetailsQuery()
            {
                UserId = UserId,
                Id = id,
            };
            var titleDetailsVM = await Mediator.Send(query);

            return Ok(titleDetailsVM);
        }

        ////создать аниме
        //[HttpPost("CreatingTitle")]
        //public async Task<ActionResult<Guid>> Post([FromBody] CreateTitleDto createTitleDto)
        //{
        //    var command = _mapper.Map<CreateTitleCommand>(createTitleDto);
        //    command.UserId = UserId;
        //    var animeId = await Mediator.Send(command);
        //    return Ok(animeId);
        //}

        ////изменить детали аниме
        //[HttpPut]
        //public async Task<ActionResult> Update([FromBody] UpdateTitleDetailsDto updateTitleDetailsDto)
        //{
        //    var command = _mapper.Map<UpdateTitleDetailsCommand>(updateTitleDetailsDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        ////добавить коллекции к аниме
        //[HttpPut]
        //public async Task<ActionResult> Update([FromBody] AddCollectionsInTitleDto addCollectionsInTitleDto)
        //{
        //    var command = _mapper.Map<AddCollectionsInTitleCommand>(addCollectionsInTitleDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        ////удалить аниме тайтлы
        //[HttpDelete]
        //public async Task<ActionResult> Delete([FromBody] DeleteTitlesDto deleteTitlesDto)
        //{
        //    var command = _mapper.Map<DeleteTitlesCommand>(deleteTitlesDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        ////удалить коллекции из аниме
        //[HttpDelete]
        //public async Task<ActionResult> Delete([FromBody] DeleteCollectionsFromTitleDto
        //    deleteCollectionsFromTitleDto)
        //{
        //    var command = _mapper.Map<DeleteCollectionsFromTitleCommand>(deleteCollectionsFromTitleDto);
        //    command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}
    }
}
