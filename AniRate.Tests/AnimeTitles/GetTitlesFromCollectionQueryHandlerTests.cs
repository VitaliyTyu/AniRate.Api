using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection;
using AniRate.Application.Common.Exceptions;
using AniRate.Application.Common.Models;
using AniRate.Infrastructure.Persistence;
using AniRate.Tests.Common;
using AutoMapper;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AniRate.Tests.AnimeTitles
{
    [Collection("QueryCollection")]
    public class GetTitlesFromCollectionQueryHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetTitlesFromCollectionQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetTitlesFromCollectionQueryHandler_Success()
        {
            // Arrange
            var handler = new GetTitlesFromCollectionQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetTitlesFromCollectionQuery
                {
                    CollectionId = ContextFactory.FirstCollectionId,
                    UserId = ContextFactory.UserAId,
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<PaginatedList<BriefTitleVM>>();
            result.TotalCount.ShouldBe(4);
        }

        [Fact]
        public async Task GetNoteListQueryHandler_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new GetTitlesFromCollectionQueryHandler(Context, Mapper);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetTitlesFromCollectionQuery
                    {
                        CollectionId = Guid.NewGuid(),
                        UserId = ContextFactory.UserAId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetNoteListQueryHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new GetTitlesFromCollectionQueryHandler(Context, Mapper);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetTitlesFromCollectionQuery
                    {
                        CollectionId = ContextFactory.FirstCollectionId,
                        UserId = ContextFactory.UserBId,
                    },
                    CancellationToken.None));
        }
    }
}
