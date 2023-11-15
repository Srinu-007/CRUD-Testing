using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class PersonsService : IPersonsService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;


        public PersonsService()
        {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
         
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse personresponse = person.ToPersonResponse();
            personresponse.Country = _countriesService.GetCountryByCountryId(person.CountryId)?.CountryName;
            return personresponse;
        }

        public PersonResponse AddPerson(PersonAddRequest? personaddrequest)
        {
            if (personaddrequest == null)
                throw new ArgumentNullException(nameof(PersonAddRequest));

            //Model Validations

            ValidationHelper.ModelValidation(personaddrequest);

            //if (personaddrequest.PersonName == null)
            //{
            //    throw new ArgumentException(nameof(personaddrequest.PersonName));
            //}
            //if (string.IsNullOrEmpty(personaddrequest.PersonName))
            //    throw new ArgumentException("Person name cannot be null");

           Person person = personaddrequest.ToPerson();

            person.PersonId = Guid.NewGuid();

            _persons.Add(person);

           return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
           return _persons.Select(temp => temp.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonByPersonId(Guid? personId)
        {
            if (personId == null)
                return null;

          Person? person = _persons.FirstOrDefault(temp=>temp.PersonId == personId);
            if(person == null)
                return null;

            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string SearchBy, string? SearchString)
        {
          List<PersonResponse> allpersons = GetAllPersons();
          List<PersonResponse> matchingpersons = allpersons;

            if (string.IsNullOrEmpty(SearchBy) || string.IsNullOrEmpty(SearchString))
                return matchingpersons;

            switch(SearchBy)
            {
                case nameof(Person.PersonName):
                    matchingpersons = allpersons.Where(temp=>
                    (!string.IsNullOrEmpty(temp.PersonName)?
                    temp.PersonName.Contains(SearchString,StringComparison.OrdinalIgnoreCase):true)).ToList();
                    break;

                case nameof(Person.Email):
                    matchingpersons = allpersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.DateOfBirth):
                    matchingpersons = allpersons.Where(temp =>
                    (temp.DateOfBirth != null) ?
                    temp.DateOfBirth.Value.ToString("dd MMMM yyyy").
                    Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    matchingpersons = allpersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Gender) ?
                    temp.Gender.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.CountryId):
                    matchingpersons = allpersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ?
                    temp.Country.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Address):
                    matchingpersons = allpersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default: matchingpersons = allpersons; break;
            }

            return matchingpersons;
        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personupdaterequest)
        {
           if(personupdaterequest == null)
                throw new ArgumentNullException(nameof(Person));

            //Validation
            ValidationHelper.ModelValidation(personupdaterequest);

            //get matching person object to update
            Person? matchingperson = _persons.FirstOrDefault(temp=>temp.PersonId == personupdaterequest.PersonId);
           
            if(matchingperson == null)
            {
                throw new ArgumentException("Given PersonId doesn't exist");
            }

            //update
            matchingperson.PersonName = personupdaterequest.PersonName;
            matchingperson.Email = personupdaterequest.Email;
            matchingperson.Gender = personupdaterequest.Gender.ToString();
            matchingperson.Address = personupdaterequest.Address;
            matchingperson.CountryId = personupdaterequest.CountryId;
            matchingperson.DateOfBirth = personupdaterequest.DateOfBirth;

            return matchingperson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personId)
        {
           if(personId == null)
                throw new ArgumentNullException(nameof(personId));

           Person? person =  _persons.FirstOrDefault(temp => temp.PersonId == personId);
            if(person == null)
                return false;

           _persons.RemoveAll(temp=> temp.PersonId == personId);

            return true;
        }
    }
}
