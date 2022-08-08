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
        public Task<List<PassportsInfo>> GetHistoryAsync(int series, int number)
        {
            var result = history.Where(s => s.Series == series && s.Number == number).ToList();
            return Task.FromResult(result);
        }

        public Task<List<PassportsInfo>> GetHistoryAsync(DateTime date)
        {
            var result = history.Where(x=>x.ChangeTime == date).ToList();
            return Task.FromResult(result);
        }

        public Task<PassportsInfo?> GetInactivePassportAsync(int series, int number)
        {
            var result = pass.FirstOrDefault(s => s.Series == series && s.Number == number);
            return Task.FromResult(result);
        }
    }
}
