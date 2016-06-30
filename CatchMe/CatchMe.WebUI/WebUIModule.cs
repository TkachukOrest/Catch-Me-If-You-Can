using System.Web.Http.Filters;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;
using CatchMe.Repositories.EF.Concrete;
using CatchMe.WebUI.Code;
using CatchMe.WebUI.Controllers;
using CatchMe.WebUI.Infrastructure.ErrorHandling;
using CatchMe.WebUI.Filters.Api;

using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.WebApi.FilterBindingSyntax;


namespace CatchMe.WebUI
{
    public class WebUIModule : NinjectModule
    {
        public override void Load()
        {
            #region DAL
            Bind<IRepositorySettings>().To<RepositorySettings>();
            Rebind<ICatchMeContext>().To<CatchMeContext>().InRequestScope();
            #endregion

            #region Error handlers
            Bind<IErrorHandler>().To<MvcErrorHandler>().WithConstructorArgument("errorControllerType", typeof(ErrorController));
            #endregion

            #region Filters
            this.BindHttpFilter<LogApiActionErrorFilter>(FilterScope.Controller)
             .WhenControllerHas<LogApiActionErrorAttribute>();

            this.BindHttpFilter<LogApiActionErrorFilter>(FilterScope.Action)
                .WhenActionMethodHas<LogApiActionErrorAttribute>();

            this.BindHttpFilter<LogApiActionFilter>(FilterScope.Global);
            #endregion
        }
    }
}