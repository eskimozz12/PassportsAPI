namespace passports.Services.PassportService
{
    public interface IPassportService
    {
        Task<Passports?> GetInactivePassportAsync(int series, int number);

        Task<List<HistoryItem>> GetHistoryAsync(int series, int number);

        Task<List<HistoryItem>> GetHistoryAsync(DateTime date);
    }
}
