using Consumer.API.DataAccess.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Consumer.API.DataAccess.Organisations
{
    public class Organisation
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
