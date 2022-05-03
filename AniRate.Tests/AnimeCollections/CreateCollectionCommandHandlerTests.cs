using AniRate.Application.AnimeCollections.Commands.CreateCollection;
using AniRate.Application.Common.Exceptions;
using AniRate.Tests.Common;
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

namespace AniRate.Tests.AnimeCollections
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
                    collection.Name == collectionName));
        }

        [Fact]
        public async Task CreateCollectionCommandHandler_CreateWithOneAnime_Success()
        {
            // Arrange
            var handler = new CreateCollectionCommandHandler(Context);
            var userId = ContextFactory.UserAId;
            var collectionName = "2 created collection";
            var animeTitlesIds = new List<Guid>();
            animeTitlesIds.Add(ContextFactory.Anime1.Id);

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
                    collection.AnimeTitles.Contains(ContextFactory.Anime1)));
        }

        [Fact]
        public async Task CreateCollectionCommandHandler_CreateWithManyAnimes_Success()
        {
            // Arrange
            var handler = new CreateCollectionCommandHandler(Context);
            var userId = ContextFactory.UserAId;
            var collectionName = "3 created collection";
            var animeTitlesIds = new List<Guid>();
            animeTitlesIds.Add(ContextFactory.Anime1.Id);
            animeTitlesIds.Add(ContextFactory.Anime2.Id);
            animeTitlesIds.Add(ContextFactory.Anime3.Id);

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
                    collection.AnimeTitles.Contains(ContextFactory.Anime1) &&
                    collection.AnimeTitles.Contains(ContextFactory.Anime2) &&
                    collection.AnimeTitles.Contains(ContextFactory.Anime3)));
        }

        [Fact]
        public async Task CreateCollectionCommandHandler_CreateWithManyAnimes_FailOnWrongId()
        {
            // Arrange
            var handler = new CreateCollectionCommandHandler(Context);
            var userId = ContextFactory.UserAId;
            var collectionName = "4 created collection";
            var animeTitlesIds = new List<Guid>();
            animeTitlesIds.Add(ContextFactory.Anime1.Id);
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
