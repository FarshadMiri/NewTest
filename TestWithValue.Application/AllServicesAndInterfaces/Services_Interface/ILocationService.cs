using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services_Interface
{
    public interface ILocationService
    {
        Task<List<DropdownItem>> GetLocationsForDropdownAsync();
        Task<Tbl_Location> GetLocationByIdAsync(int? locationId);

    }
}
