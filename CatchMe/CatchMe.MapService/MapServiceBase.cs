using System.Drawing;
using CatchMe.Infrastructure.Abstract;

namespace CatchMe.MapService
{
    public abstract class MapServiceBase
    {
        #region Fields
        protected readonly IConfigurationService ConfigurationService;        
        #endregion

        #region Constructor
        protected MapServiceBase(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }
        #endregion

        #region Methods
        public string GetApiKey()
        {
            return ConfigurationService.GetConfiguration(Constants.MapsApiDeveloperKey);
        }

        protected Size GetStaticImageSize()
        {
            var imageWidth = ConfigurationService.GetConfigurationValue<int>(Constants.StaticMapImageWidth);
            var imageHeight = ConfigurationService.GetConfigurationValue<int>(Constants.StaticMapImageHeight);

            return new Size(imageWidth, imageHeight);
        }
        #endregion
    }
}
