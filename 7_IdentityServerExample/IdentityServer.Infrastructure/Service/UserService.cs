using IdentityServer.Infrastructure.Entities;
using IdentityServer.Infrastructure.IService;
using System.Linq;

namespace IdentityServer.Infrastructure.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IdentityContext dbContext) : base(dbContext)
        {

        }

        public User Login(string phone)
        {
            return _dbcontext.Users.FirstOrDefault(x => x.Phone == phone);
        }

        public User Login(string username, string password)
        {
            return _dbcontext.Users.FirstOrDefault(x => x.UserName == username && x.Password == password);
        }

        public User LoginByExternalProvider(string provider, string identity)
        {
            return _dbcontext.Users.FirstOrDefault(x => x.Provider == provider && x.Identity == identity);
        }
    }
}
