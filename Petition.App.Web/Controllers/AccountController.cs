using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petition.App.Web.Authentication;
using Petition.App.Web.Enumerations;
using Petition.App.Web.Models;
using Petition.App.Web.Models.APIDtos;
using Petition.App.Web.Models.APIModels;
using Petition.App.Web.Models.Dtos;
using Petition.App.Web.Services;
using Petition.App.Web.Services.IServices;

namespace Petition.App.Controllers
{
    public class AccountController : Controller
    {
        protected ResponseDto _response;
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
            this._response = new ResponseDto();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<object> Register([FromBody]Register model)
        {
            try
            {
                //Validating the Request Model using data anotation
                if (ModelState.IsValid)
                {
                    //Create user
                    var registeredUser = await _userService.CreateUser(new UserRegistration(model.LastName, model.Email, model.Password));
                    if (registeredUser != null & registeredUser.statusCode == 201)
                    {
                        _response.IsSuccess = true;
                        _response.DisplayMessage = "User Created Successfully";
                    }
                    else
                    {
                       
                        _response.IsSuccess = false;
                        _response.DisplayMessage = "Unable to create account";

                    }

                }
            }

            catch (Exception ex)
            {
                //Throw exception
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            try
            {
                //Validating the Request Model using data anotation
                if (ModelState.IsValid)
                {
                    //Login
                    var user = await _userService.Login(new UserLogin(model.Email, model.Password));
                    //check if the user exist on the database
                    if (user != null & user.statusCode == 200)
                    {
                        //Convert to the User DTO
                        User resModel = JsonConvert.DeserializeObject<User>(Convert.ToString(user.value));
                        //Store the user details in the local cache.
                        SessionPersister.SetAuthentication(new Web.Models.Register()
                        {
                            Email= resModel.Email,
                            FirstName= resModel.FirstName,
                            LastName = resModel.LastName,
                            UserId = resModel.UserId,
                            RoleName = resModel.RoleId == 2 ? "user" : "admin"
                        });
                        //Store the accessToken in the local cache.
                        SessionPersister.setUseAccessToken(resModel.token);
                        //Store the Email in the local cache.
                        SessionPersister.setUserEmail(resModel.Email);
                        _response.IsSuccess = true;
                        _response.DisplayMessage = "Login Successfully";
                    }
                    else
                    {
                        _response.IsSuccess = false;
                        _response.DisplayMessage = "Invalid Login Credentials";
                    }

                }
            }

            catch (Exception ex)
            {
                //Throw exception
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;

        }
    }
}
