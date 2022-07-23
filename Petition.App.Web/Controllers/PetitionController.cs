using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petition.App.Web.Authentication;
using Petition.App.Web.Models;
using Petition.App.Web.Models.Dtos;
using Petition.App.Web.Services.IServices;

namespace Petition.App.Controllers
{
    public class PetitionController : Controller
    {
        protected ResponseDto _response;
        private readonly IPetitionService _petitionService;
        private readonly string _accessToken;
        private static IHttpContextAccessor _httpContextAccessor => new HttpContextAccessor();
        public PetitionController(IPetitionService petitionService)
        {
            this._accessToken = SessionPersister.LoggedInAccessToken;
            this._response = new ResponseDto();
            _petitionService = petitionService; 
        }
        public async Task<IActionResult> Index()
        {
            //Get data from Cache using Http Context accessor
            string email = SessionPersister.LoggedInEmail;
            TempData["Email"] = email;

            //check for access token
            if(string.IsNullOrEmpty(_accessToken))
                return RedirectToAction("Login","Account");
            var petitionDetails = await  _petitionService.GetPetition(_accessToken);
            //check if petition exist
            if(petitionDetails != null & petitionDetails.statusCode == 200)
            {
                //Convert to the Petition DTO
                List<Value> resModel = JsonConvert.DeserializeObject<List<Value>>(Convert.ToString(petitionDetails.value));
                var model = resModel?.Where(c => c.createdByEmail == email).ToList();
                if (model != null)
                {
                    List<PetitionDto> petitionDtos = new();
                    foreach (var item in model)
                    {
                        petitionDtos.Add(new PetitionDto
                        {
                            CreatedBy = item.createdBy,
                            Description = item.details,
                            PetitionId = item.petitionId,
                            Title = item.name,
                            TotalCount = item.userPetitions.Count()
                        });

                    }
                    return View(petitionDtos);
                }
            }
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
                //Validating the Request Model using data anotation
                if (ModelState.IsValid)
                {
                    //Get data from Cache using Http Context accessor
                    string email = SessionPersister.LoggedInEmail;
                    model.CreatedBy = email;
                    var user = SessionPersister.LoggedInUsers;
                    //check if petition was created
                    var petitionDetails = await _petitionService.CreatePetition(new Web.Models.APIDtos.PetitionDTO
                    {
                        Name = model.Title,
                        Details = model.Description,
                        CreatedBy = user.UserId,
                    }, _accessToken);
                    if (petitionDetails != null & petitionDetails.statusCode == 201)
                    {
                        _response.IsSuccess = true;
                        _response.DisplayMessage = "Petition Created Successfully";
                    }
                    else
                    {
                        _response.IsSuccess = false;
                        _response.DisplayMessage = "Error Occured While creating petition";
                    }

                }
            }

            catch (Exception ex)
            {
                //Throw execption
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
