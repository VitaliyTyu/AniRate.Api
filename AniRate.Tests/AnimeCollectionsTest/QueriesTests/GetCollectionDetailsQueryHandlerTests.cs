using AniRate.Application.AnimeCollections.Queries;
using AniRate.Application.AnimeCollections.Queries.GetCollectionDetails;
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

namespace AniRate.Tests.AnimeCollectionsTest.QueriesTests
{
    [Collection("QueryCollection")]
    public class GetCollectionDetailsQueryHandlerTests
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetCollectionDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCollectionDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCollectionDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCollectionDetailsQuery
                {
                    UserId = ContextFactory.UserBId,
                    Id = ContextFactory.ThirdCollectionId,
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CollectionDetailsVM>();
            result.Name.ShouldBe("3 collection");
            result.AnimeTitles.Items.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetCollectionDetailsQueryHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new GetCollectionDetailsQueryHandler(Context, Mapper);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                new GetCollectionDetailsQuery
                {
                    UserId = ContextFactory.UserAId,
                    Id = ContextFactory.ThirdCollectionId,
                },
                CancellationToken.None));
        }

        [Fact]
        public async Task GetCollectionDetailsQueryHandler_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new GetCollectionDetailsQueryHandler(Context, Mapper);

            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                new GetCollectionDetailsQuery
                {
                    UserId = ContextFactory.UserBId,
                    Id = Guid.NewGuid(),
                },
                CancellationToken.None));
        }
    }
}
