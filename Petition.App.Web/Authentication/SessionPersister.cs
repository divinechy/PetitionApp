using Newtonsoft.Json;
using Petition.App.Web.Models;
using Petition.App.Web.Models.Dtos;

namespace Petition.App.Web.Authentication
{
    public class SessionPersister
    {
        private static IHttpContextAccessor _httpContextAccessor => new HttpContextAccessor();
        #region IsAuthenticated
        public static bool IsAuthenticated
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("Users") != null)
                {
                    return true;
                }
                return false;
            }
        }
        public static void setUserEmail(string email)
        {
            _httpContextAccessor.HttpContext.Session.SetString("email", email);
        }
        public static string LoggedInEmail
        {
            get
            {
                var users = _httpContextAccessor.HttpContext?.Session?.GetString("email");
                if (users != null)
                    return users;
                return null;
            }
        }

        public static void setUseAccessToken(string token)
        {
            _httpContextAccessor.HttpContext.Session.SetString("accessToken", token);
        }
        public static string LoggedInAccessToken
        {
            get
            {
                var users = _httpContextAccessor.HttpContext?.Session?.GetString("accessToken");
                if (users != null)
                    return users;
                return null;
            }
        }

        public static Register LoggedInUsers
        {
            get
            {
                var users = _httpContextAccessor.HttpContext?.Session?.GetString("Users");
                if(users != null)
                    return JsonConvert.DeserializeObject<Register>(users);
                return null;

            }
        }


        public static List<PetitionDto> Petitions
        {
            get
            {
                var users = _httpContextAccessor.HttpContext?.Session?.GetString("Petitions");
                if (users != null)
                    return JsonConvert.DeserializeObject<List<PetitionDto>>(users);
                return null;

            }
        }

        #endregion


        public static void SetPetition(List<PetitionDto> petitions)
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            _httpContextAccessor.HttpContext.Session.SetString("Petitions", JsonConvert.SerializeObject(petitions));
        }
        public static List<CauseUserDto> causeUserDtos
        {
            get
            {
                var users = _httpContextAccessor.HttpContext?.Session?.GetString("CauseUserDto");
                if (users != null)
                    return JsonConvert.DeserializeObject<List<CauseUserDto>>(users);
                return null;

            }
        }

        public static void SetUserCause(List<CauseUserDto> causeUsers)
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            _httpContextAccessor.HttpContext.Session.SetString("CauseUserDto", JsonConvert.SerializeObject(causeUsers));
        }

        public static void SetAuthentication(Register user)
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            _httpContextAccessor.HttpContext.Session.SetString("Users", JsonConvert.SerializeObject(user));
        }

    }
}
