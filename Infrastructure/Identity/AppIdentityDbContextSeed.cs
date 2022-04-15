using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManger)
        {
            if (!userManger.Users.Any()) // !를 넣었으므로 만약 Users 
            {
                var user = new AppUser
                {
                    //아래의 Email, UserName은 AppUser에서 Implements를 했던 IdentityUser.cs 가 가지고 있는 own properties 들임. 그렇기 때문에, Email, UserName을 property로 선언 안했지만, 자동으로 사용 가능 함. 
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210",
                    },
                };

                await userManger.CreateAsync(user, "Pa$$w0rd");
            }

        }
    }
}