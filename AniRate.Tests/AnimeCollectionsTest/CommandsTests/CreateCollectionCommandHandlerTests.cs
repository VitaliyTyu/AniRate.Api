using AniRate.Application.AnimeCollections.Commands.CreateCollection;
using AniRate.Application.Common.Exceptions;
using AniRate.Tests.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AniRate.Tests.AnimeCollectionsTest.CommandsTests
{
    public class CreateCollectionCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateCollectionCommandHandler_CreateEmpty_Success()
        {
            // Arrange
            var handler = new CreateCollectionCommandHandler(Context);
            var userId = ContextFactory.UserAId;
            var collectionName = "1 created collection";
            var animeTitlesIds = new List<Guid>();

            // Act
            var collectionId = await handler.Handle(
                new CreateCollectionCommand
                {
                    UserId = userId,
                    Name = collectionName,
                    AnimeTitlesIds = animeTitlesIds,
                },
                CancellationToken.None);


            //Assert
            Assert.NotNull(
                await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                    collection.Id == collectionId &&
                    collection.Name == collectionName &&
                    collection.Image == null));
        }


        [Fact]
        public async Task CreateCollectionCommandHandler_CreateWithOneAnime_Success()
        {
            // Arrange
            var handler = new CreateCollectionCommandHandler(Context);
            var userId = ContextFactory.UserAId;
            var collectionName = "2 created collection";
            var animeTitlesIds = new List<Guid>();
            animeTitlesIds.Add(ContextFactory.FirstAnimeId);

            // Act
            var collectionId = await handler.Handle(
                new CreateCollectionCommand
                {
                    UserId = userId,
                    Name = collectionName,
                    AnimeTitlesIds = animeTitlesIds,
                },
                CancellationToken.None);

            //Assert
            var anime = await Context.AnimeTitles.SingleOrDefaultAsync(a => a.Id == ContextFactory.FirstAnimeId);

            Assert.NotNull(
                await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                    collection.Id == collectionId &&
                    collection.Name == collectionName &&
                    collection.AnimeTitles.Any(a => a.Id == ContextFactory.FirstAnimeId) &&
                    collection.Image == anime.Image));
        }


        [Fact]
        public async Task CreateCollectionCommandHandler_CreateWithManyAnimes_Success()
        {
            // Arrange
            var handler = new CreateCollectionCommandHandler(Context);
            var userId = ContextFactory.UserAId;
            var collectionName = "3 created collection";
            var animeTitlesIds = new List<Guid>();
            animeTitlesIds.Add(ContextFactory.FirstAnimeId);
            animeTitlesIds.Add(ContextFactory.SecondAnimeId);
            animeTitlesIds.Add(ContextFactory.ThirdAnimeId);

            // Act
            var collectionId = await handler.Handle(
                new CreateCollectionCommand
                {
                    UserId = userId,
                    Name = collectionName,
                    AnimeTitlesIds = animeTitlesIds,
                },
                CancellationToken.None);

            //Assert
            var anime = await Context.AnimeTitles.SingleOrDefaultAsync(a => a.Id == ContextFactory.FirstAnimeId);

            Assert.NotNull(
                await Context.AnimeCollections.SingleOrDefaultAsync(collection =>
                    collection.Id == collectionId &&
                    collection.Name == collectionName &&
                    collection.AnimeTitles.Any(a => a.Id == ContextFactory.FirstAnimeId) &&
                    collection.AnimeTitles.Any(a => a.Id == ContextFactory.SecondAnimeId) &&
                    collection.AnimeTitles.Any(a => a.Id == ContextFactory.ThirdAnimeId) &&
                    collection.Image == anime.Image));
        }


        [Fact]
        public async Task CreateCollectionCommandHandler_CreateWithManyAnimes_FailOnWrongId()
        {
            // Arrange
            var handler = new CreateCollectionCommandHandler(Context);
            var userId = ContextFactory.UserAId;
            var collectionName = "4 created collection";
            var animeTitlesIds = new List<Guid>();
            animeTitlesIds.Add(ContextFactory.FirstAnimeId);
            animeTitlesIds.Add(Guid.NewGuid());

            // Act

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new CreateCollectionCommand
                    {
                        UserId = userId,
                        Name = collectionName,
                        AnimeTitlesIds = animeTitlesIds,
                    },
                    CancellationToken.None));
        }
    }
}
