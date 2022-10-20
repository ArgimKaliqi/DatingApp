using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            //checking if there is any users
            if(await context.Users.AnyAsync()) return;

            //Getting the json file
            var userData= await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            //Deserializing it into an object
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            
            //Hardcodeing the password for each user generated using HMACSHA512 
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                  context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}