using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PassportsAPI.EfCore;

namespace passports.Services.PassportService
{
    public class PassportService : IPassportService
    {

        private static List<PassportsInfo> pass = new List<PassportsInfo>
        {
            new PassportsInfo{Series = 1111, Number = 123451, ChangeTime = new DateTime(2022, 8, 8), IsActive = false},
            new PassportsInfo{Series = 1121, Number = 123451, ChangeTime = new DateTime(2022, 8, 8), IsActive = false}
        };
        private static List<PassportsInfo> history = new List<PassportsInfo>
        {
            new PassportsInfo{Series = 1111, Number = 123451, ChangeTime = new DateTime(2022, 8, 8), IsActive = false},
            new PassportsInfo{Series = 1121, Number = 123451, ChangeTime = new DateTime(2022, 9, 8), IsActive = false},
            new PassportsInfo{Series = 3221, Number = 123451, ChangeTime = new DateTime(2022, 8, 8), IsActive = false},
            new PassportsInfo{Series = 1111, Number = 123451, ChangeTime = new DateTime(2022, 9, 8), IsActive = true}
        };

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PassportService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<PassportsInfo>> GetHistoryAsync(int series, int number)
        {
            var result = _context.PassportsHistory.Where(m => m.Series == series && m.Number == number).Select(m => new PassportsInfo
            {
                Series = m.Series,
                Number = m.Number,
                IsActive = m.IsActive,
                ChangeTime = m.ChangeTime
            }).ToList();
            return Task.FromResult(result);
        }

        public Task<List<PassportsInfo>> GetHistoryAsync(DateTime date)
        {
            var result = _context.PassportsHistory.Where(x => x.ChangeTime == date).Select(m => new PassportsInfo
            {
                Series = m.Series,
                Number = m.Number,
                IsActive = m.IsActive,
                ChangeTime = m.ChangeTime
            }).ToList();
            return Task.FromResult(result);

        }

        public Task<InactivePassports> GetInactivePassportAsync(int series, int number)
        {
            var tmp = _context.IanctivePassports.Where(m => m.Series == series && m.Number == number);
            var result = _mapper.Map<InactivePassports>(tmp);
            return Task.FromResult(result);
        }
    }
}
