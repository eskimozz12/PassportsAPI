using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PassportsAPI.EfCore;

namespace passports.Services.PassportService
{
    public class PassportService : IPassportService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PassportService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PassportsInfo>> GetHistoryAsync(int series, int number)
        {
            var result = await _context.PassportsHistory
                .Include(m => m.Passport)
                .Where(m => m.Passport.Series == series && m.Passport.Number == number)
                .OrderBy(x=>x.ChangeTime)
                .ToListAsync();
            return _mapper.Map<List<PassportsInfo>>(result);
        }

        public async Task<List<PassportsInfo>> GetHistoryAsync(DateTime date)
        {
            date = date.ToUniversalTime();
            var result = await _context.PassportsHistory
                .Include(m => m.Passport)
                .Where(x => x.ChangeTime.Date == date.Date)
                .ToListAsync();
            return _mapper.Map<List<PassportsInfo>>(result);
        }

        public async Task<PassportsInfo> GetInactivePassportAsync(int series, int number)
        {
            var tmp = await _context.InanctivePassports
                .FirstOrDefaultAsync(m => m.Series == series && m.Number == number);
            var result = _mapper.Map<PassportsInfo>(tmp);
            return result;
        }
    }
}
