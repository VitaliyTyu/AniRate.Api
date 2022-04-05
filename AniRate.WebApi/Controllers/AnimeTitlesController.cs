using AniRate.Application.AnimeTitles.Queries.GetAnimeTitles;
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

        //[HttpGet]
        //public async Task<ActionResult<List<AnimeTitleBriefDto>>> GetAll([FromQuery] GetAnimeTitlesQuery query)
        //{
        //    var animeTitles = await Mediator.Send(query);

        //    return Ok(animeTitles);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<List<AnimeTitleBriefDto>>> GetFromCollection(Guid id)
        //{
        //    var query = new GetAnimeTitlesQuery()
        //    {
        //        CollectionId = id
        //    };
        //    var animeTitles = await Mediator.Send(query);

        //    return Ok(animeTitles);
        //}

        [HttpGet]
        public async Task<ActionResult<AnimeTitlesListVM>> Get()
        {
            var query = new GetAnimeTitlesQuery()
            {
                CollectionId = Guid.Parse("936da01f-9abd-4d9d-80c7-02af85c822a2")
            };
            var animeTitlesVM = await Mediator.Send(query);

            return Ok(animeTitlesVM);
        }

    }
}
