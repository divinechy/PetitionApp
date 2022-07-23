namespace Petition.App.Web.Models.Dtos
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = false;
        public object value { get; set; }
        public string DisplayMessage { get; set; }
        public int statusCode { get; set; }
        public object contentType { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
