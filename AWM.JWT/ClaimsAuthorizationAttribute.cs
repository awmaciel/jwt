using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;

namespace AWM.JWT
{
    public class ClaimsAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            var identity = (ClaimsIdentity)principal.Identity;
           
            if (!principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<string>("Faca o login");
            }

            if ((principal.HasClaim(x => x.Type == ClaimType && x.Value == ClaimValue)))
            {                
                return Task.FromResult<object>(principal);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse((HttpStatusCode.Unauthorized), "Voce nao tem permissao para executar essa url:  " + actionContext.Request.RequestUri);
                return Task.FromResult<object>(null);
            }
            
        }
    }
}