using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithValue.Application.Contract.Persistence;
using TestWithValue.Domain.Enitities;

namespace TestWithValue.Data.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly TestWithValueDbContext _context;

        public LocationRepository(TestWithValueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tbl_Location>> GetAllLocationsAsync()
        {
            return await _context.tbl_Locations.ToListAsync();
        }
        public async Task<Tbl_Location> GetLocationByIdAsync(int? locationId)
        {
            if (locationId == null) return null;
            return await _context.tbl_Locations.FindAsync(locationId);
        }

    }
}
