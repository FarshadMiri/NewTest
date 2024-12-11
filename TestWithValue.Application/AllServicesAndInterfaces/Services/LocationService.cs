using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.AllServicesAndInterfaces.Services_Interface;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Application.AllServicesAndInterfaces.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<DropdownItem>> GetLocationsForDropdownAsync()
        {
            var locations = await _locationRepository.GetAllLocationsAsync();
            return locations.Select(loc => new DropdownItem
            {
                Value = loc.LocationId.ToString(),
                Text = loc.Name
            }).ToList();
        }

        public async Task<Tbl_Location> GetLocationByIdAsync(int? locationId)
        {
            return await _locationRepository.GetLocationByIdAsync(locationId);
        }

    }
}
