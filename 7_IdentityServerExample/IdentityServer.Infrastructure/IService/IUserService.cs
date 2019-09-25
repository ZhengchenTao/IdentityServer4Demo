using IdentityServer.Infrastructure.Entities;

namespace IdentityServer.Infrastructure.IService
{
    public interface IUserService
    {
        User Login(string phone);
        User Login(string username, string password);
        User LoginByExternalProvider(string provider, string subid);
    }
}
