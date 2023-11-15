using ServiceContracts.DTO;

namespace ServiceContracts
{
/// <summary>
/// Represents business logic for manipulating Country entity
/// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Method that adds Country object into list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// <returns>Returns country object after adding it</returns>
        /// 

        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Returns a list of CountryResponse items
        /// </summary>
        /// <returns></returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Returns Country object based on countryId
        /// </summary>
        /// <param name="countryId">CountryId to search</param>
        /// <returns>Matching country object</returns>
        CountryResponse? GetCountryByCountryId(Guid? countryId);
    }
}