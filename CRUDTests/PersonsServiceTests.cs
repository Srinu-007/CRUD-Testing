using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Entities;
using System.Reflection;

namespace CRUDTests
{
    public class PersonsServiceTests
    {
        private readonly IPersonsService _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _testOutputHelper;


        public PersonsServiceTests(ITestOutputHelper testOutputHelper)
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService();
            _testOutputHelper = testOutputHelper;
        }

        #region AddPerson


        //Null value as PersonAddRequest
        [Fact]
        public void AddPerson_NullPerson()
        {
            PersonAddRequest? personAddRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.AddPerson(personAddRequest);
            });
        }

        //When PersonName is Null
        [Fact]
        public void AddPerson_NullPersonName()
        {
            PersonAddRequest? personAddrequest_personName = new PersonAddRequest() { PersonName = null };

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.AddPerson(personAddrequest_personName);
            });
        }

        //When PersonName is correct
        [Fact]
        public void AddPerson_ProperPersonDetails()
        {
            PersonAddRequest? personAddrequest_properpersondetails = new PersonAddRequest()
            {
                PersonName = "PersonName...",
                Email = "Person@email.com",
                Address = "sample address",
                CountryId = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Now,
                ReceiveNewsLetters = true,
            };

            PersonResponse person_responce_from_add = _personsService.AddPerson(personAddrequest_properpersondetails);
            List<PersonResponse> persons_list = _personsService.GetAllPersons();

            Assert.True(person_responce_from_add.PersonId != Guid.Empty);
            Assert.Contains(person_responce_from_add, persons_list);
        }

        #endregion

        #region GetPersonById
        //If we supply null as PersonId,it should return null as PersonResponse
        [Fact]
        public void GetPersonByPersonId_Null()
        {
            Guid? personId = null;
            PersonResponse? personresponsefromnull = _personsService.GetPersonByPersonId(personId);
            Assert.Null(personresponsefromnull);
        }

        //If we supply valid PersonId,it should return corresponding persondetails.
        [Fact]
        public void GetPersonByPersonId_validId()
        {
            CountryAddRequest country_request = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse country_response = _countriesService.AddCountry(country_request);

            PersonAddRequest personaddrequest = new PersonAddRequest()
            {
                PersonName = "person name..",
                Email = "person@email.com",
                Address = "Sample address",
                CountryId = country_response.CountryId,
                DateOfBirth = DateTime.Parse("2000-01-10"),
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };

            PersonResponse person_response_from_add = _personsService.AddPerson(personaddrequest);

            PersonResponse? person_response_fropm_get = _personsService.GetPersonByPersonId(person_response_from_add.PersonId);

            Assert.Equal(person_response_from_add, person_response_from_add);
        }
        #endregion

        #region GetAllPersons
        //The GetAllPersons() SHOULD RETURN EMPTY LIST
        [Fact]
        public void GetAllPersons_EmptyList()
        {
            List<PersonResponse> person_response_from_get = _personsService.GetAllPersons();

            Assert.Empty(person_response_from_get);
        }

        [Fact]
        public void GetAllPersons_AddFewPersons()
        {
            CountryAddRequest country_add_request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_add_request2 = new CountryAddRequest() { CountryName = "CHINA" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_add_request1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_add_request2);

            PersonAddRequest person_add_request1 = new PersonAddRequest()
            {
                PersonName = "SmithName...",
                Email = "Smith@email.com",
                Address = "Smith address",
                CountryId = country_response_1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Now,
                ReceiveNewsLetters = true,
            };

            PersonAddRequest person_add_request2 = new PersonAddRequest()
            {
                PersonName = "WarnerName...",
                Email = "Warner@email.com",
                Address = "Warner address",
                CountryId = country_response_1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Now,
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_add_request1, person_add_request2 };

            List<PersonResponse> person_response_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest personAddRequest in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(personAddRequest);
                person_response_from_add.Add(person_response);
            }

            List<PersonResponse> person_response_from_get = _personsService.GetAllPersons();

            foreach (PersonResponse personresponsefromadd in person_response_from_add)
            {
                Assert.Contains(personresponsefromadd, person_response_from_get);
            }
        }
        #endregion

        #region GetFilteredPersons
        //If searchtext is empty and search by is PersonName...it should return all persoins list
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            CountryAddRequest country_add_request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest country_add_request2 = new CountryAddRequest() { CountryName = "CHINA" };

            CountryResponse country_response_1 = _countriesService.AddCountry(country_add_request1);
            CountryResponse country_response_2 = _countriesService.AddCountry(country_add_request2);

            PersonAddRequest person_add_request1 = new PersonAddRequest()
            {
                PersonName = "SmithName...",
                Email = "Smith@email.com",
                Address = "Smith address",
                CountryId = country_response_1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Now,
                ReceiveNewsLetters = true,
            };

            PersonAddRequest person_add_request2 = new PersonAddRequest()
            {
                PersonName = "WarnerName...",
                Email = "Warner@email.com",
                Address = "Warner address",
                CountryId = country_response_1.CountryId,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Now,
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_add_request1, person_add_request2 };

            List<PersonResponse> person_response_from_add = new List<PersonResponse>();

            foreach (PersonAddRequest personAddRequest in person_requests)
            {
                PersonResponse person_response = _personsService.AddPerson(personAddRequest);
                person_response_from_add.Add(person_response);
            }

            List<PersonResponse> person_response_from_search = _personsService.GetFilteredPersons(nameof(Person.PersonName), "");

            foreach (PersonResponse personresponsefromadd in person_response_from_add)
            {
                Assert.Contains(personresponsefromadd, person_response_from_search);
            }

        }
            //First We will add few persons and then we will search  based on person name witth search string.It should return matching persons
            [Fact]
            public void GetFilteredPersons_SearchByPersonName()
            {
                CountryAddRequest country_add_request1 = new CountryAddRequest() { CountryName = "USA" };
                CountryAddRequest country_add_request2 = new CountryAddRequest() { CountryName = "CHINA" };

                CountryResponse country_response_1 = _countriesService.AddCountry(country_add_request1);
                CountryResponse country_response_2 = _countriesService.AddCountry(country_add_request2);

                PersonAddRequest person_add_request1 = new PersonAddRequest()
                {
                    PersonName = "SmithName...",
                    Email = "Smith@email.com",
                    Address = "Smith address",
                    CountryId = country_response_1.CountryId,
                    Gender = GenderOptions.Male,
                    DateOfBirth = DateTime.Now,
                    ReceiveNewsLetters = true,
                };

                PersonAddRequest person_add_request2 = new PersonAddRequest()
                {
                    PersonName = "WarnerName...",
                    Email = "Warner@email.com",
                    Address = "Warner address",
                    CountryId = country_response_1.CountryId,
                    Gender = GenderOptions.Male,
                    DateOfBirth = DateTime.Now,
                    ReceiveNewsLetters = true,
                };

                List<PersonAddRequest> person_requests = new List<PersonAddRequest>() { person_add_request1, person_add_request2 };

                List<PersonResponse> person_response_from_add = new List<PersonResponse>();

                foreach (PersonAddRequest personAddRequest in person_requests)
                {
                    PersonResponse person_response = _personsService.AddPerson(personAddRequest);
                    person_response_from_add.Add(person_response);
                }

                List<PersonResponse> person_response_from_search = _personsService.GetFilteredPersons(nameof(Person.PersonName), "na");

                foreach (PersonResponse personresponsefromadd in person_response_from_add)
                {
                    if(personresponsefromadd.PersonName != null)
                    {
                        if (personresponsefromadd.PersonName.Contains("na", StringComparison.OrdinalIgnoreCase))
                        {
                            Assert.Contains(personresponsefromadd, person_response_from_search);
                        }
                    }

                }
            }
        #endregion

        #region UpdatePerson
        //Wwhen we supply null as PersonUpdateRequest,it should throw Argument Null Exception
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            PersonUpdateRequest? personUpdateRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });
           
        }

        //WHEN WE SUPPLY INVALID PERSONID,IT SHOULD THROW ARGUMENT EXCEPTION
        [Fact]
        public void UpdatePerson_InvalidPersonId()
        {
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
            {
                PersonId = Guid.NewGuid()
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });

        }

        //When Person Name is null it should throw ArgumentException
        [Fact]
        public void UpdatePerson_NullPersonName()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse country_response_from_add = _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest person_add_request = new PersonAddRequest()
            {
                PersonName = "John",
                CountryId = country_response_from_add.CountryId,
                Address = "some address",
                Email = "john@gmail.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true,
                Gender = GenderOptions.Male
            };
            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.Topersonupdaterequest();

            person_update_request.PersonName = null;

            
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(person_update_request);
            });

        }

        //First we will add a new person and then try to update the same and email
        [Fact]
        public void UpdatePerson_PersonFullDetails()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "UK" };

            CountryResponse country_response_from_add = _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest person_add_request = new PersonAddRequest() { PersonName = "John", CountryId = country_response_from_add.CountryId,Address="some address",
            Email="john@gmail.com",DateOfBirth=DateTime.Parse("2000-01-01"),ReceiveNewsLetters=true,Gender = GenderOptions.Male};

            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

            PersonUpdateRequest person_update_request = person_response_from_add.Topersonupdaterequest();

            person_update_request.PersonName = "William";
            person_update_request.Email = "William@gmail.com";

            PersonResponse person_response_from_update = _personsService.UpdatePerson(person_update_request);

            PersonResponse? person_response_from_get = _personsService.GetPersonByPersonId(person_response_from_update.PersonId);

            Assert.Equal(person_response_from_get, person_response_from_update);

        }
        #endregion

        #region DeletePerson
        //If PersonId is valid,,it should return true
        [Fact]
        public void DeletePerson_validPersonId()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "USA" };

            CountryResponse country_response_from_ADD = _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest person_add_request = new PersonAddRequest() {
                PersonName = "John",
                CountryId = country_response_from_ADD.CountryId,
                Address = "some address",
                Email = "john@gmail.com",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true,
                Gender = GenderOptions.Male
            };

            PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

           bool isDeleted = _personsService.DeletePerson(person_response_from_add.PersonId);

            Assert.True(isDeleted);
        }


        //If PersonId is valid,,it should return false
        [Fact]
        public void DeletePerson_invalidPersonId()
        {
            bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());

            Assert.False(isDeleted);
        }

        #endregion
    }
}
