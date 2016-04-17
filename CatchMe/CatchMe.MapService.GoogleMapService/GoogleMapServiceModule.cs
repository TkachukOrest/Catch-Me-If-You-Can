using Ninject.Modules;

namespace CatchMe.MapService.GoogleMapService
{
    public class GoogleMapServiceModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IMapService>().To<GoogleMapService>();
        }
    }
}
