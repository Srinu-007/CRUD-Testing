namespace Entities
{
    public class Country
    {
    /// <summary>
    /// Domain model for storing Country details
    /// </summary>
        public Guid CountryId { get; set; }

        public string? CountryName { get; set; }
    }
}