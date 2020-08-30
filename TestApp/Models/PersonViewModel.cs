using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class PersonViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "სახელი სავალდებულოა")]
        public string Name { get; set; }

        [Required(ErrorMessage = "გვარი სავალდებულოა")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "დაბადების თარიღი სავალდებულოა")]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "პინი სავალდებულოა")]
        public string Pin { get; set; }

        [Required(ErrorMessage = "ნომერი სავალდებულოა")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "საიტი სავალდებულოა")]
        public string Website { get; set; }

        [Required(ErrorMessage = "ქვეყანა სავალდებულოა")]
        public int? CountryId { get; set; }

        public string CountryString { get; set; }


        public string Email { get; set; }
        [Required(ErrorMessage = "სქესის მითითება აუცლებელია")]

        public int? GenderId { get; set; }
        public string GenderString { get; set; }
    }
}
