namespace IdentityServer.Infrastructure.Service
{
    public class BaseService
    {
        protected IdentityContext _dbcontext;
        public BaseService(IdentityContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
    }
}
