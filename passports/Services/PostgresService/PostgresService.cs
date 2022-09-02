using Microsoft.EntityFrameworkCore;
using PassportsAPI.EfCore;
using System.Text;

namespace PassportsAPI.Services.PostgresService
{
    public class PostgresService
    {
        private readonly DataContext _context;
        public PostgresService(DataContext context)
        {
            _context = context;
        }

        public async Task BeginUpdateAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("COPY temptable(series, number) FROM 'Data/data.csv' DELIMITER ',' CSV HEADER; ");
        }
        public async Task UploadAsync()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                var sb = new StringBuilder();
                sb.AppendLine("INSERT INTO passports(series,number,isactive,changetime)");
                sb.AppendLine(" SELECT series, number,'false', now() FROM temptable");
                sb.AppendLine("ON CONFLICT (series, number) WHERE isactive = 'true' DO UPDATE");
                sb.AppendLine(" SET isactive = 'false', changetime = now()");

                await _context.Database.ExecuteSqlRawAsync(sb.ToString());

                sb.Clear();
                sb.AppendLine("UPDATE passports SET isactive = 'true', changetime = now() WHERE(series, number) NOT IN(SELECT series, number FROM temptable)");

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
