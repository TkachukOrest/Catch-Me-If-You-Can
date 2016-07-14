namespace CatchMe.Repositories.EF.Abstract
{
    public class EfRepository
    {
        protected readonly ICatchMeContext CatchMeContext;

        public EfRepository(ICatchMeContext context)
        {
            CatchMeContext = context;
        }
    }
}
