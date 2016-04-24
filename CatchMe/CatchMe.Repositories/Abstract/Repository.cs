namespace CatchMe.Repositories.Abstract
{
    public abstract class DataBaseRepository
    {
        protected IRepositorySettings _repositorySettings;

        public DataBaseRepository(IRepositorySettings repositorySettings)
        {
            _repositorySettings = repositorySettings;
        }
    }
}
