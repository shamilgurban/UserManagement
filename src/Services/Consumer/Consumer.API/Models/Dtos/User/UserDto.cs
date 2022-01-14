using Consumer.API.Models.Dtos.Organisation;

namespace Consumer.API.Models.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? OrganisationId { get; set; }
        public OrganisationDto Organisation { get; set; }
    }
}
