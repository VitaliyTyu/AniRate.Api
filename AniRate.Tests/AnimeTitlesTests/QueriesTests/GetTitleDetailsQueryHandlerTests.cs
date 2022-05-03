using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.AnimeTitles.Queries.GetTitleDetails;
using AniRate.Application.Common.Exceptions;
using AniRate.Infrastructure.Persistence;
using AniRate.Tests.Common;
using AutoMapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AniRate.Tests.AnimeTitlesTests.QueriesTests
{
    [Collection("QueryCollection")]
    public class GetTitleDetailsQueryHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetTitleDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetTitleDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetTitleDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetTitleDetailsQuery
                {
                    Id = Guid.Parse("0F1FD77C-7A9D-4E74-86F0-B5597C95A262"),
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<TitleDetailsVM>();
            result.Name.ShouldBe("1 аниме");
            result.Score.ShouldBe("10");
        }

        [Fact]
        public async Task GetTitleDetailsQueryHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new GetTitleDetailsQueryHandler(Context, Mapper);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                new GetTitleDetailsQuery
                {
                    Id = Guid.Parse("0F1FD77C-7A9D-4E74-86F0-B5597C95A263"),
                },
                CancellationToken.None));
        }
    }
}
