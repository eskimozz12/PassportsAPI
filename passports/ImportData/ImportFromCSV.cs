using Microsoft.EntityFrameworkCore;
using Npgsql;
using PassportsAPI.EfCore;
using System.Data;

namespace PassportsAPI.ImportData
{
    public class ImportFromCSV
    {
        private readonly string sql = "COPY passports(series, number) FROM 'C:/Users/pasha/Desktop/data.csv' DELIMITER ',' CSV HEADER ";
        private readonly DataContext _context;
        public ImportFromCSV(DataContext context)
        {
            _context = context;
        }
        public async Task<int> ImportAsync()
        {
            return await _context.Database.ExecuteSqlRawAsync(sql);
        }
            
    }
}
