using AniRate.Application.AnimeCollections.Commands.DeleteManyTitlesFromCollection;
using AniRate.Application.Common.Exceptions;
using AniRate.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AniRate.Tests.AnimeCollectionsTest.CommandsTests
{
    public class DeleteManyTitlesFromCollectionCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteManyTitlesFromCollectionCommandHandler(Context);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);


            // Act
            await handler.Handle(
                new DeleteManyTitlesFromCollectionCommand
                {
                    AnimeTitlesIds = animesIds,
                    Id = ContextFactory.FirstCollectionId,
                    UserId = ContextFactory.UserAId,
                },
                CancellationToken.None);


            //Assert
            foreach (var animeId in animesIds)
            {
                Assert.Null(
                    await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                        collection.Id == ContextFactory.FirstCollectionId &&
                        collection.AnimeTitles.Any(a => a.Id == animeId)));
            }
        }

        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_FailOnWrongCollectionId()
        {
            // Arrange
            var handler = new DeleteManyTitlesFromCollectionCommandHandler(Context);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteManyTitlesFromCollectionCommand
                    {
                        AnimeTitlesIds = animesIds,
                        Id = Guid.NewGuid(),
                        UserId = ContextFactory.UserAId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_FailOnWrongAnimeId()
        {
            // Arrange
            var handler = new DeleteManyTitlesFromCollectionCommandHandler(Context);

            var animesIds = new List<Guid>();
            animesIds.Add(Guid.NewGuid());
            animesIds.Add(ContextFactory.SecondAnimeId);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteManyTitlesFromCollectionCommand
                    {
                        AnimeTitlesIds = animesIds,
                        Id = ContextFactory.FirstCollectionId,
                        UserId = ContextFactory.UserAId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new DeleteManyTitlesFromCollectionCommandHandler(Context);

            var animesIds = new List<Guid>();
            animesIds.Add(ContextFactory.FirstAnimeId);
            animesIds.Add(ContextFactory.SecondAnimeId);


            // Act


            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteManyTitlesFromCollectionCommand
                    {
                        AnimeTitlesIds = animesIds,
                        Id = ContextFactory.FirstCollectionId,
                        UserId = ContextFactory.UserBId,
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteManyTitlesFromCollectionCommandHandler_FailOnEmptyAnimesId()
        {
            // Arrange
            var handler = new DeleteManyTitlesFromCollectionCommandHandler(Context);

            var animesIds = new List<Guid>();

            // Act

            //Assert
            await Assert.ThrowsAsync<EmptyStateException>(async () =>
                await handler.Handle(
                    new DeleteManyTitlesFromCollectionCommand
                    {
                        AnimeTitlesIds = animesIds,
                        Id = ContextFactory.FirstCollectionId,
                        UserId = ContextFactory.UserAId,
                    },
                    CancellationToken.None));
        }
    }
}
