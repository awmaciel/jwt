using Microsoft.Owin.Security.OAuth;
using AWM.JWT.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AWM.JWT.Provider
{
    public class OauthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            using (var db = new Context())
            {
                if (db != null)
                {
                    var user = db.ApiUsers.Where(o => o.UserName == context.UserName && o.UserPasswd == context.Password).FirstOrDefault();
                    if (user != null)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, user.UserRole));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                        string[] roles = user.ClaimValue.Split(',');
                        foreach (var item in roles)
                        {
                            identity.AddClaim(new Claim(user.ClaimType, item));
                        }

                        identity.AddClaim(new Claim(user.ClaimType,user.ClaimValue));
                        identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                        await Task.Run(() => context.Validated(identity));
                    }
                    else
                    {
                        context.SetError("Wrong Crendtials", "Provided username and password is incorrect");
                    }
                }
                else
                {
                    context.SetError("Wrong Crendtials", "Provided username and password is incorrect");
                }
                return;
            }
        }

    }
}