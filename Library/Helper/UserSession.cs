using CoreLibrary.Models;
using CoreLibrary.Models.Interfaces;
using Library.Helper.Interfaces;
using Newtonsoft.Json;

namespace Library.Helper
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void CreateUserSession(IUser user) {
            string value = JsonConvert.SerializeObject(user);
            _httpContextAccessor.HttpContext.Session.SetString(HelperData.GetUserSessionKey, value);
        }

        public IUser GetUserSession() {
            string userSession = _httpContextAccessor.HttpContext.Session.GetString(HelperData.GetUserSessionKey);
            
            if (string.IsNullOrEmpty(userSession))
                return null;

            return JsonConvert.DeserializeObject<User>(userSession);
        }

        public void RemoveUserSession() {
            _httpContextAccessor.HttpContext.Session.Remove(HelperData.GetUserSessionKey);
        }
    }
}
