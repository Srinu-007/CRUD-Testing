using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry
        [Fact]
        //When countryAddRequest is null,,it should throw ArgumentNullException
        public void AddCountry_NullException()
        {
            CountryAddRequest? request = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _countriesService.AddCountry(request);
            });  
        }

        [Fact]
        //When countryName is null,,it should throw ArgumentException
        public void CountryName_NullException()
        {
            CountryAddRequest? request = new CountryAddRequest() { CountryName = null };


            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(request);
            });
        }

        [Fact]
        //When countryName is duplicate,,it should throw ArgumentException
        public void DuplicateCountryName()
        {
            CountryAddRequest? request1 = new CountryAddRequest() { CountryName = "USA" };
            CountryAddRequest? request2 = new CountryAddRequest() { CountryName = "USA" };


            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }

        [Fact]
        //When CountryAddRequest is satisfied,it should throw ArgumentException
        public void AddCountry_ProperCountryDetails()
        {
            CountryAddRequest? request = new CountryAddRequest() { CountryName = "USA" };

           CountryResponse response = _countriesService.AddCountry(request);

            Assert.True(response.CountryId != Guid.Empty);
            
        }

        #endregion

        #region GetAllCountries

        [Fact]
        // The list of countries should be empty by default

        public void GetAllCountries_EmptyList()
        {
          List<CountryResponse> actual_response_countries_list =  _countriesService.GetAllCountries();

        Assert.Empty(actual_response_countries_list);

        }
        #endregion

        #region GetCountryByCountryId
        [Fact]
        //If we suppply null as countryId..it should return null as CountryResponse

        public void GetCountryByCountryId_nullCountryId()
        {
            Guid? countryId = null;

           CountryResponse? country_response_from_get_method = _countriesService.GetCountryByCountryId(countryId);

            Assert.Null(country_response_from_get_method);
        }

        [Fact]
        //If we suppply valid countryId..it should return corresponding CountryResponse
        public void GetCountryByCountryId_ValidCountryId()
        {
            CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "China" };
           CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);
           CountryResponse? country_response_from_get = _countriesService.GetCountryByCountryId(country_response_from_add.CountryId);
            Assert.Equal(country_response_from_add, country_response_from_get);
        }
        #endregion

    }
}
