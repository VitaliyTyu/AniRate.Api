using AutoMapper;
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
    public class AnimesControllers : ApiControllerBase
    {
        private readonly IMapper _mapper;
        public AnimesControllers(IMapper mapper) => _mapper = mapper;


        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[HttpGet]
        //public async Task<ActionResult<TitlesListVM>> GetAll()
        //{
        //    var query = new GetTitlesQuery()
        //    {
        //        UserId = Guid.Empty,
        //    };
        //    var animeTitlesVM = await Mediator.Send(query);

        //    return Ok(animeTitlesVM);
        //}
    }
}
