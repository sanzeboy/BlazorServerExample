using PopulationPortal.Domain.Entities.Populations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationPortal.Application.Services.Populations.Models.Filters
{
    public class PopulationFilter
    {
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public GenderEnum? Gender { get; set; }
        public AgeGroupEnum? AgeGroup { get; set; }
    }
}
