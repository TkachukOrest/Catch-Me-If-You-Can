namespace CatchMe.Repositories.Abstract
{
    public abstract class Repository
    {
        protected IRepositorySettings _repositorySettings;

        public Repository(IRepositorySettings repositorySettings)
        {
            _repositorySettings = repositorySettings;
        }
    }
}
