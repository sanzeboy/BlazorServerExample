using PopulationPortal.Domain.Entities.Addresses;
using PopulationPortal.Domain.Entities.Common;

namespace PopulationPortal.Domain.Entities.Populations
{
    public class Population : CommonProperties
    {
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public AgeGroupEnum AgeGroup { get; set; }
        public long Male { get; set; }
        public long Female { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
    }
}
