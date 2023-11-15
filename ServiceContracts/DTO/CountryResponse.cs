using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class used as return type for most of Countries service methods.
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryId { get; set; } 

        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(CountryResponse))
                return false;

            CountryResponse country = (CountryResponse)obj;
            return CountryId == country.CountryId && CountryName == country.CountryName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtensions
    {
        //Converts A Country object into CountryResponse type
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
            };

        }

    }

}
