using PopulationPortal.Domain.Entities.Common;

namespace PopulationPortal.Domain.Entities.Addresses
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
