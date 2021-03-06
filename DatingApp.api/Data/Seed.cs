using DatingApp.api.Models;
using DatingApp.api.Models.Data;
using Newtonsoft.Json;

namespace DatingApp.api.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }
        public void SeedUsers(){
            var UserData = System.IO.File.ReadAllText("Data/UserDataSeed.json");
            var users = JsonConvert.DeserializeObject<List <User>>(UserData);
            foreach(var user in users)
            {
                    byte[] passwordHash,passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt= passwordSalt;
                    user.Username= user.Username.ToLower();
                    _context.Users.Add(user);
            }
            _context.SaveChanges();
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
              using(var hmac = new System.Security.Cryptography.HMACSHA512())
              {
                  passwordSalt=hmac.Key;
                  passwordHash= hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
              }
        }
    }

}