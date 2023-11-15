using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Represents DTO class that contains person details to update
    /// </summary>
    /// 

    public class PersonUpdateRequest
    {
        [Required(ErrorMessage = "PERSON ID CANNOT BE BLANK")]
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "PERSON NAME CANNOT BE BLANK")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "EMAIL CANNOT BE BLANK")]
        [EmailAddress(ErrorMessage = "Email value should be valid email")]
        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public GenderOptions? Gender { get; set; }

        public Guid? CountryId { get; set; }

        public string? Address { get; set; }

        public bool ReceiveNewsLetters { get; set; }

        //Converts the current Object of PersonAddRequest into Object of Person type
        public Person ToPerson()
        {
            return new Person
            {
                PersonId = PersonId,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                CountryId = CountryId,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetters,
                Gender = Gender.ToString(),
            };
    }
}
}
