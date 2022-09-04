using Microsoft.EntityFrameworkCore;
using PassportsAPI.EfCore;
using System.Text;

namespace PassportsAPI.Services.PassportUpdateService
{
    public class PassportUpdateService : IPassportUpdateService
    {
        private readonly DataContext _context;
        public PassportUpdateService(DataContext context)
        {
            _context = context;
        }

        public async Task BeginUpdateAsync()
        {
            string path = Path.GetFullPath("Data\\data.csv");
            await _context.Database.ExecuteSqlRawAsync("COPY temptable(series, number) FROM '"+ path +"' DELIMITER ',' CSV HEADER; ");
        }
        public async Task UploadAsync()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                var sb = new StringBuilder();
                sb.AppendLine("INSERT INTO passports(series,number,isactive,changetime)");
                sb.AppendLine(" SELECT series, number,'false', now()::timestamp FROM temptable");
                sb.AppendLine("ON CONFLICT (series, number) WHERE isactive = 'true' DO UPDATE");
                sb.AppendLine(" SET isactive = 'false', changetime = now()::timestamp");

                await _context.Database.ExecuteSqlRawAsync(sb.ToString());

                sb.Clear();
                sb.AppendLine("UPDATE passports SET isactive = 'true', changetime = now()::timestamp WHERE(series, number) NOT IN(SELECT series, number FROM temptable)");

                await _context.Database.ExecuteSqlRawAsync(sb.ToString());

                _context.SaveChanges();

                dbContextTransaction.Commit();
            }
        
        }
        public async Task EndUpdateAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM temptable");
        }
    }
}
