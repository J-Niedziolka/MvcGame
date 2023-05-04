using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcGame.Data;
using System;
using System.Linq;

namespace MvcGame.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcGameContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcGameContext>>()))
        {
            // Look for any movies.
            if (context.Game.Any())
            {
                return;   // DB has been seeded
            }
            context.Game.AddRange(
                new Game
                {
                    Tytul = "The Last of Us",
                    Data_wydania = DateTime.Parse("2013-6-14"),
                    Gatunek = "Akcja",
                    Wydawca = "Naughty Dog",
                    Cena = 60
                },
                new Game
                {
                    Tytul = "League of Legends",
                    Data_wydania = DateTime.Parse("2009-10-27"),
                    Gatunek = "MOBA",
                    Wydawca = "RIOT",
                    Cena = 0
                },
                new Game
                {
                    Tytul = "Wiedźmin 3: Dziki Gon",
                    Data_wydania = DateTime.Parse("2015-5-18"),
                    Gatunek = "RPG",
                    Wydawca = "CDProjekt RED",
                    Cena = 29
                },
                new Game
                {
                    Tytul = "Stardew Valley",
                    Data_wydania = DateTime.Parse("2016-2-26"),
                    Gatunek = "Indie",
                    Wydawca = "Chucklefish",
                    Cena = 19
                },
                 new Game
                 {
                     Tytul = "The Elder Scrolls V: Skyrim",
                     Data_wydania = DateTime.Parse("2011-11-11"),
                     Gatunek = "RPG",
                     Wydawca = "Bethesda",
                     Cena = 9
                 },
                  new Game
                  {
                      Tytul = "Doom",
                      Data_wydania = DateTime.Parse("1993-12-10"),
                      Gatunek = "Akcja",
                      Wydawca = "id Software",
                      Cena = 2
                  },
                  new Game
                  {
                      Tytul = "Minecraft",
                      Data_wydania = DateTime.Parse("2011-11-18"),
                      Gatunek = "Gra survivalowa",
                      Wydawca = "Mojang Studios",
                      Cena = 30
                  }
            );
            context.SaveChanges();
        }
    }
}