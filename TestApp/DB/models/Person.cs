using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.DB.models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Pin { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public Country Country { get; set; }
        public int? CountryId { get; set; }
        public string Email { get; set; }
        public int? GenderId { get; set; }
        public Gender Gender { get; set; }

    }
}
