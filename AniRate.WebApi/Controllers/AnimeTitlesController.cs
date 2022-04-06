using AniRate.Application.AnimeTitles.Queries.GetAnimeTitles;
using AniRate.WebApi.Models;
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


        //Получение всех аниме в конкретной коллекции
        [HttpGet("titles/{id}")]
        public async Task<ActionResult<AnimeTitlesListVM>> Get(Guid id)
        {
            var query = new GetAnimeTitlesQuery()
            {
                UserId = UserId,
                CollectionId = id
            };
            var animeTitlesVM = await Mediator.Send(query);

            return Ok(animeTitlesVM);
        }

        //Получение деталей определенного аниме
        [HttpGet("details/{id}")]
        public async Task<ActionResult<AnimeTitlesListVM>> Get(string id)
        {
            var query = new GetAnimeTitlesQuery()
            {
                UserId = UserId,
                CollectionId = Guid.Parse(id)
            };
            var animeTitlesVM = await Mediator.Send(query);

            return Ok(animeTitlesVM);
        }

        //создать аниме
        //[HttpPost]
        //public async Task<ActionResult<Guid>> Post([FromBody] CreateAnimeTitleDto createAnimeTitleDto)
        //{
        //    var command = _mapper.Map<CreateAnimeTitleCommand>(createAnimeTitleDto);
        //    //command.UserId = UserId;
        //    var animeId = await Mediator.Send(command);
        //    return Ok(animeId);
        //}

        ////изменить детали аниме
        //[HttpPut]
        //public async Task<ActionResult> Update([FromBody] UpdateAnimeTitleDto updateAnimeTitleDto)
        //{
        //    var command = _mapper.Map<UpdateAnimeTitleCommand>(updateAnimeTitleDto);
        //    //command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        ////добавить коллекции к аниме
        //[HttpPut]
        //public async Task<ActionResult> Update([FromBody] UpdateAnimeTitleDto updateAnimeTitleDto)
        //{
        //    var command = _mapper.Map<UpdateAnimeTitleCommand>(updateAnimeTitleDto);
        //    //command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        //удалить аниме
        //[HttpDelete]
        //public async Task<ActionResult> Delete([FromBody] DeleteAnimeTitleDto deleteAnimeTitleDto)
        //{
        //    var command = _mapper.Map<DeleteAnimeTitleCommand>(deleteAnimeTitleDto);
        //    //command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}

        //удалить коллекции из аниме
        //[HttpDelete]
        //public async Task<ActionResult> Delete([FromBody] DeleteAnimeTitleDto deleteAnimeTitleDto)
        //{
        //    var command = _mapper.Map<DeleteAnimeTitleCommand>(deleteAnimeTitleDto);
        //    //command.UserId = UserId;
        //    await Mediator.Send(command);
        //    return NoContent();
        //}
    }
}
