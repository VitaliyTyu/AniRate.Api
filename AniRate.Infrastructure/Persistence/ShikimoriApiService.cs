using AniRate.Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AniRate.Infrastructure.Persistence
{
    public class BriefAnime
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class ShikimoriApiService
    {
        public static async Task ParseAnimeTitles(ApplicationDbContext context)
        {
            
            using (HttpClient client = new HttpClient())
            {
                Log.Information("Аниме до: " + context.AnimeTitles.Count());
                Log.Information("Жанров до: " + context.Genres.Count());

                var page = context.AnimeTitles.Count() / 50;
                for (int i = page + 1; i < page + 2; i++)
                {
                    try
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        var StreamTask1 = client.GetStreamAsync($"https://shikimori.one/api/animes?&order=ranked&limit=50&page={i}");
                        await Task.Delay(700);
                        var animes = await JsonSerializer.DeserializeAsync<List<BriefAnime>>(await StreamTask1);

                        foreach (var Id in animes.Select(a => a.Id))
                        {

                            var StreamTask2 = client.GetStreamAsync($"https://shikimori.one/api/animes/{Id}");

                            await Task.Delay(700);
                            var anime = await JsonSerializer.DeserializeAsync<AnimeTitle>(await StreamTask2);

                            anime.Id = Guid.NewGuid();
                            anime.Image.Id = Guid.NewGuid();
                            anime.Image.AnimeId = anime.Id;
                            anime.UserId = Guid.Empty;
                            //anime.CollectionId = Guid.Empty;

                            if (anime.Genres != null)
                                foreach (var genre in anime.Genres)
                                {
                                    genre.Id = Guid.NewGuid();
                                    genre.AnimeId = anime.Id;
                                }

                            await context.AnimeTitles.AddAsync(anime);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Information(ex, "An error occurred ShikimoriApiService running");
                        Log.Information("Аниме после ошибки: " + context.AnimeTitles.Count());
                        Log.Information("Жанров после ошибки: " + context.Genres.Count());

                        await Task.Delay(300);
                    }

                    await context.SaveChangesAsync();
                }
                Log.Information("Аниме после: " + context.AnimeTitles.Count());
                Log.Information("Жанров после: " + context.Genres.Count());
            }
        }
    }
}
