using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petition.App.Authentication;
using Petition.App.Enumerations;
using Petition.App.Models;
using Petition.App.Models.Dtos;

namespace Petition.App.Controllers
{
    public class AccountController : Controller
    {
        protected ResponseDto _response;
        public AccountController()
        {
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
                if (ModelState.IsValid)
                {
                    var registeredUser = SessionPersister.LoggedInUsers;
                    if (registeredUser != null)
                    {
                        registeredUser.Add(model);
                         SessionPersister.SetAuthentication(registeredUser);
                        _response.IsSuccess = true;
                        _response.DisplayMessage = "User Created Successfully";
                    }
                    else
                    {
                        registeredUser = new();
                        registeredUser.Add(model);
                        SessionPersister.SetAuthentication(registeredUser);
                        _response.IsSuccess = true;
                        _response.DisplayMessage = "User Created Successfully";

                    }

                }
            }

            catch (Exception ex)
            {
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
                if (ModelState.IsValid)
                {
                    var user =  SessionPersister.LoggedInUsers;
                    if (user != null)
                    {
                        var isValid = user.Where(c => c.Email == model.Email && c.Password == model.Password).FirstOrDefault();
                        if(isValid != null)
                        {
                            SessionPersister.setUserEmail(isValid.Email);
                            _response.IsSuccess = true;
                            _response.DisplayMessage = "User Created Successfully";
                        }
                        else
                        {
                            _response.IsSuccess = false;
                            _response.DisplayMessage = "Invalid Login Details";
                        }
                        
                    }

                }
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;

        }
    }
}
