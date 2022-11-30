using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security
{
    public class UserAccessor : IUserAccessor //create srvice that will return our login user
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor; //stores the request and response information
        }

        public string GetUsername() //implement the interface
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name); //get the user that make this request.
        }
    }
}