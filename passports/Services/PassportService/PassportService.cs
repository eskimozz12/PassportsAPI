namespace passports.Services.PassportService
{
    public class PassportService : IPassportService
    {

        private static List<Passports> pass = new List<Passports>
        {
            new Passports{Series = 1111, Number = 123451, ChangeTime = new DateTime(2022, 8, 8), IsActive = false},
            new Passports{Series = 1121, Number = 123451, ChangeTime = new DateTime(2022, 8, 8), IsActive = false}
        };
        private static List<HistoryItem> history = new List<HistoryItem>
        {
            new HistoryItem{Series = 1111, Number = 123451, ChangeTime = new DateTime(2022, 8, 8), IsActive = false},
            new HistoryItem{Series = 1121, Number = 123451, ChangeTime = new DateTime(2022, 9, 8), IsActive = false},
            new HistoryItem{Series = 3221, Number = 123451, ChangeTime = new DateTime(2022, 8, 8), IsActive = false},
            new HistoryItem{Series = 1111, Number = 123451, ChangeTime = new DateTime(2022, 9, 8), IsActive = true}
        };
        public async Task<List<HistoryItem>> GetHistoryAsync(int series, int number)
        {
            var result = history.Where(s => s.Series == series && s.Number == number).ToList();
            return result;
        }

        public async Task<List<HistoryItem>> GetHistoryAsync(DateTime date)
        {
            var result = history.Where(x=>x.ChangeTime == date).ToList();
            return result;
        }

        public async Task<Passports?> GetInactivePassportAsync(int series, int number)
        {
            return pass.FirstOrDefault(s => s.Series == series && s.Number == number);
        }
    }
}
