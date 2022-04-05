using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        //internal Guid UserId => !User.Identity.IsAuthenticated
        //    ? Guid.Empty
        //    : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        internal Guid UserId => Guid.Parse("936da01f-9abd-4d9d-80c7-02af85c822a8");
    }
}
