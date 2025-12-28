using Microsoft.EntityFrameworkCore;
using Conference_Room_Rental.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Conference_Room_Rental.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var adminEmail = "admin@admin.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        FirstName = "Admin",
                        LastName = "Administrator",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin123!");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }

                if (context.ConferenceRooms.Any())
                {
                    return;
                }

                context.ConferenceRooms.AddRange(
                    new ConferenceRoom
                    {
                        RoomNumber = "Sala Narad (Mała)",
                        Location = "Piętro 1, Skrzydło A",
                        Description = "Kameralna salka idealna na spotkania rekrutacyjne lub szybkie burze mózgów.",
                        Capacity = 6,
                        PricePerHour = 50.00m,
                        HasProjector = false,
                        HasAirConditioning = true,
                        HasWiFi = true,
                        HasWhiteboard = true,
                        IsActive = true,
                        ImageUrl = "/images/Meeting-Room1.jpg"
                    },
                    new ConferenceRoom
                    {
                        RoomNumber = "Sala Konferencyjna 101",
                        Location = "Piętro 2, Centrum",
                        Description = "Standardowa sala szkoleniowa z pełnym wyposażeniem multimedialnym.",
                        Capacity = 25,
                        PricePerHour = 120.00m,
                        HasProjector = true,
                        HasAirConditioning = true,
                        HasWiFi = true,
                        HasWhiteboard = true,
                        IsActive = true,
                        ImageUrl = "/images/Meeting-Room2.jpg"
                    },
                    new ConferenceRoom
                    {
                        RoomNumber = "Aula Główna (VIP)",
                        Location = "Parter, Główne Wejście",
                        Description = "Prestiżowa przestrzeń na duże konferencje, wykłady i eventy firmowe. Nagłośnienie w cenie.",
                        Capacity = 150,
                        PricePerHour = 450.00m,
                        HasProjector = true,
                        HasAirConditioning = true,
                        HasWiFi = true,
                        HasWhiteboard = false,
                        IsActive = true,
                        ImageUrl = "/images/Meeting-Room3.jpg"
                    },
                    new ConferenceRoom
                    {
                        RoomNumber = "Sala Warsztatowa B",
                        Location = "Piętro 1, Skrzydło B",
                        Description = "Sala przystosowana do pracy grupowej, stoły modułowe.",
                        Capacity = 12,
                        PricePerHour = 80.00m,
                        HasProjector = true,
                        HasAirConditioning = false,
                        HasWiFi = true,
                        HasWhiteboard = true,
                        IsActive = true,
                        ImageUrl = "/images/Meeting-Room4.jpg"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}