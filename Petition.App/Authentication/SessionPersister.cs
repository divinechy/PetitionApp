using Newtonsoft.Json;
using Petition.App.Models;
using Petition.App.Models.Dtos;

namespace Petition.App.Authentication
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

        public static List<Register> LoggedInUsers
        {
            get
            {
                var users = _httpContextAccessor.HttpContext?.Session?.GetString("Users");
                if(users != null)
                    return JsonConvert.DeserializeObject<List<Register>>(users);
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

        public static void SetAuthentication(List<Register> user)
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
            _httpContextAccessor.HttpContext.Session.SetString("Users", JsonConvert.SerializeObject(user));
        }

    }
}
