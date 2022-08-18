﻿using PassportsAPI.EfCore;

namespace passports.Services.PassportService
{
    public interface IPassportService
    {
        Task<InactivePassports> GetInactivePassportAsync(int series, int number);

        Task<List<PassportsInfo>> GetHistoryAsync(int series, int number);

        Task<List<PassportsInfo>> GetHistoryAsync(DateTime date);
    }
}
