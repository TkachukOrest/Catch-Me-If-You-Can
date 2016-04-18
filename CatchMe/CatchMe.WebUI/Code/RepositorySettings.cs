using System.Configuration;
using CatchMe.Repositories.Abstract;

namespace CatchMe.WebUI.Code
{
    public class RepositorySettings : IRepositorySettings
    {
        #region Consts
        private const string ConnectionStringName = "DefaultConnection";
        #endregion

        #region IRepositorySettings
        public string ConnectionString => ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        #endregion
    }
}