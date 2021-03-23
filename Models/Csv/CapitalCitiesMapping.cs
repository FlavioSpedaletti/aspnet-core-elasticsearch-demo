using CsvHelper.Configuration;
using System.Globalization;

namespace KhalidAbuhakmeh.AspNetCore.Search.Models.Csv
{
    public class CapitalCitiesMapping : ClassMap<CapitalCityRecord>
    {
        public CapitalCitiesMapping()
        {
            Map(x => x.City).Name("city");
            Map(x => x.CityAscii).Name("city_ascii");
            Map(x => x.Latitude).ConvertUsing(d => decimal.Parse(d.GetField("lat"), CultureInfo.GetCultureInfo("en-US"))).Name("lat");
            Map(x => x.Longitude).ConvertUsing(d => decimal.Parse(d.GetField("lng"), CultureInfo.GetCultureInfo("en-US"))).Name("lng");
            Map(x => x.Country).Name("country");
            Map(x => x.Population).Name("population");
            Map(x => x.Id).Name("id");
        }
    }

    public class CapitalCityRecord
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string CityAscii { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Country { get; set; }
        public int? Population { get; set; }
    }
}