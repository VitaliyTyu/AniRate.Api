using AniRate.Application.AnimeCollections.Queries.GetAnimeCollections;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
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

        [HttpGet]
        public async Task<ActionResult<AnimeCollectionsListVM>> GetAll()
        {
            var query = new GetAnimeCollectionsQuery()
            {
                UserId = UserId
            };
            var collectionsVM = await Mediator.Send(query);
            return Ok(collectionsVM);
        }
    }
}
