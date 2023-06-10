using JobPortal.API.Data;
using JobPortal.API.Models.Enums;
using JobPortal.API.Models;

namespace JobPortal.API.Helpers.Seeder
{
    public class UserSeeder
    {
        private readonly PortalContext _portalContext;

        public UserSeeder(PortalContext portalContext)
        {
            _portalContext = portalContext;
        }
        public void SeedInitialUsers()
        {
            if (!_portalContext.Users.Any()) // verificam daca in tabela noastra exista users
            {
                var user1 = new User
                {
                    UserId = 1,
                    EmployeeId = 1,
                    Email = "user1@gmail.com",
                    PasswordHash = "1234",
                    PasswordSalt = "1234",
                    PhoneNumber = "1234567890",
                    Role = Role.user
                };
                var user2 = new User
                {
                    UserId = 2,
                    EmployeeId = 2,
                    Email = "user2@gmail.com",
                    PasswordHash = "1234",
                    PasswordSalt = "1234",
                    PhoneNumber = "1234567890",
                    Role = Role.user
                };
                _portalContext.Add(user1);
                _portalContext.Add(user2);

                _portalContext.SaveChanges();

                /*
                var userData = System.IO.File.ReadAllText("Helpers/Seeders/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.UserName = user.UserName.ToLower();

                    _context.Users.Add(user);
                }
             */
            }
        }
    }
}
