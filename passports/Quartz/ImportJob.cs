using PassportsAPI.EfCore;
using Quartz;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PassportsAPI.Quartz
{
    public class ImportJob : IJob
    {
        private readonly DataContext _context;
        private readonly ILogger<ImportJob> _logger;
        public ImportJob(ILogger<ImportJob> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(@"Server=localhost;Database=PassportsDB;Port=5432;User Id=postgres;Password=12345"))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "COPY temptable(series, number) FROM 'C:/Users/pasha/Desktop/data.csv' DELIMITER ',' CSV HEADER;";

                    cmd.ExecuteNonQuery();
                }
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "CREATE OR REPLACE FUNCTION history()" +
                        " RETURNS TRIGGER" +
                        " AS $$" +
                        " BEGIN" +
                        " IF TG_OP = 'INSERT' THEN" +
                        " INSERT INTO history(isactive,changetime,passportid) VALUES (NEW.isactive, NEW.changetime, NEW.id);" +
                        " RETURN NEW;" +
                        " ELSIF TG_OP = 'UPDATE' THEN" +
                        " IF NEW.isactive <> OLD.isactive THEN" +
                        " INSERT INTO history(isactive,changetime,passportid) VALUES (NEW.isactive, NEW.changetime, OLD.id);" +
                        " END IF;" +
                        " RETURN NEW;" +
                        " END IF;" +
                        " END;" +
                        " $$ LANGUAGE plpgsql;";
                    cmd.ExecuteNonQuery();
                }
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                       " DROP TRIGGER IF EXISTS tr_history on passports;";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =
                        " CREATE TRIGGER tr_history AFTER UPDATE OR INSERT ON passports" +
                        " FOR EACH ROW EXECUTE PROCEDURE history();";
                    cmd.ExecuteNonQuery();
                }

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                         "INSERT INTO passports(series,number,isactive,changetime) SELECT series, number,'false', now() FROM temptable ON CONFLICT (series, number) WHERE isactive = 'true' DO" +
                         " UPDATE SET isactive = 'false', changetime = now()";
                    cmd.ExecuteNonQuery();
                }
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                         "UPDATE passports SET isactive = 'true', changetime = now() WHERE (series,number) NOT IN (SELECT series,number FROM temptable)";
                    cmd.ExecuteNonQuery();
                }


                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "DELETE FROM temptable";

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

            _logger.LogInformation("ImportData");
            return Task.CompletedTask;
        }
    }
}
