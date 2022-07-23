using Microsoft.AspNetCore.Mvc;
using Petition.App.Authentication;
using Petition.App.Models.Dtos;

namespace Petition.App.Controllers
{
    public class PetitionController : Controller
    {
        protected ResponseDto _response;
        private static IHttpContextAccessor _httpContextAccessor => new HttpContextAccessor();
        public PetitionController()
        {
            this._response = new ResponseDto();
        }
        public IActionResult Index()
        {
            string email = SessionPersister.LoggedInEmail;
            TempData["Email"] = email;
            var petitionDetails = SessionPersister.Petitions;
            var model = petitionDetails?.Where(c => c.CreatedBy == email);
            if (model != null)
            {
                var causeUserModel = SessionPersister.causeUserDtos;
                if (causeUserModel != null)
                {
                    foreach (var item in model)
                    {
                        item.TotalCount = causeUserModel.Where(c => c.PetitionId == item.PetitionId).Count();
                    }
                }
                return View(model);
            }
            else
                return View();
        }

        public IActionResult CreatePetition()
        {
            string email = SessionPersister.LoggedInEmail;
            TempData["Email"] = email;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");
            return View();
        }
        [HttpPost]
        public async Task<object> CreatePetition([FromBody]PetitionDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string email = SessionPersister.LoggedInEmail;
                    model.CreatedBy = email;
                    var petitionDetails = SessionPersister.Petitions;
                    if (petitionDetails != null)
                    {
                        model.PetitionId = petitionDetails.Count() + 1;
                        if (model.PetitionId % 2 == 0)
                            model.ImageUrl = "/images/11.jpg";
                        else
                            model.ImageUrl = "/images/13.jpg";
                        petitionDetails.Add(model);
                        SessionPersister.SetPetition(petitionDetails);
                        _response.IsSuccess = true;
                        _response.DisplayMessage = "Petition Created Successfully";
                    }
                    else
                    {
                        petitionDetails = new();
                        model.PetitionId = 1;
                        model.ImageUrl = "/images/14.jpg";
                        petitionDetails.Add(model);
                        SessionPersister.SetPetition(petitionDetails);
                        _response.IsSuccess = true;
                        _response.DisplayMessage = "Petition Created Successfully";
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
        public IActionResult EditPetition()
        {
            string email = SessionPersister.LoggedInEmail;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Index", "Home");
            return View();
        }
    }
}
