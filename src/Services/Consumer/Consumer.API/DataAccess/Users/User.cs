using Consumer.API.DataAccess.Organisations;
using System.ComponentModel.DataAnnotations;

namespace Consumer.API.DataAccess.Users
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string FatherName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public int? OrganisationId { get; set; }
        public Organisation Organisation { get; set; }
    }
}
