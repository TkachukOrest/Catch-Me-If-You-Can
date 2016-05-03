using System;
using CatchMe.Security.Models;
using CatchMe.Security.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace CatchMe.Security
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {            
            app.CreatePerOwinContext<IdentityUserManager>(IdentityUserManager.Create);

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/OAuth/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(5),                
                AllowInsecureHttp = true
            };

            app.UseOAuthBearerTokens(OAuthOptions);            
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "852925537385-9t4d8fg869kpi1dlm58v77277b70lc6e.apps.googleusercontent.com",
                ClientSecret = "078iMDZvE2JKYZc8-a5TeEey",
                Provider = new GoogleAuthProvider()
            });
        }
    }
}
