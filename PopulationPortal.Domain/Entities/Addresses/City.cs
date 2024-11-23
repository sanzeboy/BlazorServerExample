using PopulationPortal.Domain.Entities.Common;

namespace PopulationPortal.Domain.Entities.Addresses
{
    public class City : BaseEntity
    {

        public int CountryId { get; set; }

        public string Name { get; set; }
        public virtual Country Country{ get; set; }
    }
}
