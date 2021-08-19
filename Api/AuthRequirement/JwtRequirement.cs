using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.AuthRequirement
{
    public class JwtRequirement : IAuthorizationRequirement { }

    public class JwtRequirementHandler : AuthorizationHandler<JwtRequirement>
    {
        private readonly HttpClient _client;
        private readonly HttpContext _httpContext;

        public JwtRequirementHandler(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            // sending the token to server
            _client = httpClientFactory.CreateClient();
            // getting the token
            _httpContext = httpContextAccessor.HttpContext; 
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            JwtRequirement requirement)
        {
            // to check if the token is present or not in the header
            if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader)) 
            {
                var accessToken = authHeader.ToString().Split(' ')[1];

                var response = await _client
                    .GetAsync($"https://localhost:44382/oauth/validate?access_token={accessToken}");

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
