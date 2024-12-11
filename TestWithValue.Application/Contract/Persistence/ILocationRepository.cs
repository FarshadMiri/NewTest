using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.Contract.Persistence
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Tbl_Location>> GetAllLocationsAsync();
        Task<Tbl_Location> GetLocationByIdAsync(int? locationId);

    }
}
