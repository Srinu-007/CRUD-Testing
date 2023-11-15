using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }

        //Converts the current Object of CountryAddRequest into Object of Country type
        public Country ToCountry()
        {
            return new Country { CountryName = CountryName };
        }
    }
}
