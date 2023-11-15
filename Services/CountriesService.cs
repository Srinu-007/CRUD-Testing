using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country>  _Countries;

        public CountriesService()
        {
            _Countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            //validation :CountryAddRequest cannot be null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
             //Validation: CountryName cannot be null

            if(countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            //validation: Duplicate CountryName should not exist

            if(_Countries.Where(temp=>temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given Country Name Already Exists");
            }

            //validation : Add Country details
            Country country = countryAddRequest.ToCountry();

            country.CountryId = Guid.NewGuid();

            _Countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
         return _Countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryId(Guid? countryId)
        {
            if (countryId == null)
                return null;

            Country? country_response_from_list = _Countries.FirstOrDefault(country => country.CountryId == countryId);

            if(country_response_from_list == null)
                return null;

            return country_response_from_list.ToCountryResponse();
        }
    }
}