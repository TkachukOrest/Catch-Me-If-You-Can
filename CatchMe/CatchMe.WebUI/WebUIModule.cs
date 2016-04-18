using CatchMe.Repositories.Abstract;
using CatchMe.WebUI.Code;
using Ninject.Modules;

namespace CatchMe.WebUI
{
    public class WebUIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositorySettings>().To<RepositorySettings>();
        }
    }
}