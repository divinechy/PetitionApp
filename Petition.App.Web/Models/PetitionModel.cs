namespace Petition.App.Web.Models
{
    public class PetitionModel
    {
        public int PetitionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }


    public class PetitionVM
    {
        public Value[] value { get; set; }
        public int statusCode { get; set; }
        public object contentType { get; set; }
    }

    public class Value
    {
        public int petitionId { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public string createdByEmail { get; set; }
        public Userpetition[] userPetitions { get; set; }
    }

    public class Userpetition
    {
        public int userPetitionId { get; set; }
        public int signedBy { get; set; }
        public string signedEmail { get; set; }
        public object remarks { get; set; }
        public DateTime signedDate { get; set; }
        public int petitionId { get; set; }
    }

}
