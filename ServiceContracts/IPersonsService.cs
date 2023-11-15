using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonsService
    {
        /// <summary>
        /// Adds new person into the existing list of persons
        /// </summary>
        /// <param name="personaddrequest"></param>
        /// <returns></returns>
        PersonResponse AddPerson(PersonAddRequest? personaddrequest);

        /// <summary>
        /// Returns all existing persons from data source or from table
        /// </summary>
        /// <returns></returns>
        List<PersonResponse> GetAllPersons();

        /// <summary>
        /// Returns Matching Person Object
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        PersonResponse? GetPersonByPersonId(Guid? personId);

        /// <summary>
        /// Returns all the personresponse that matches the search field and search test
        /// </summary>
        /// <param name="SearchBy">Search field to search</param>
        /// <param name="SearchString">Search string to search</param>
        /// <returns>Returns all the matching persons</returns>
        List<PersonResponse> GetFilteredPersons(string SearchBy, string? SearchString);

        /// <summary>
        /// Updates specified person details based on given personId
        /// </summary>
        /// <param name="personupdaterequest">Person details to update,including personId</param>
        /// <returns>Person Object after Updation</returns>
        PersonResponse UpdatePerson(PersonUpdateRequest? personupdaterequest);

        /// <summary>
        /// Deletes a person based on id supplied
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>Returns true if deletion is succesfull</returns>
        bool DeletePerson(Guid? personId);
    }
}
