using PassportsAPI.EfCore;
using Quartz;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PassportsAPI.Services.PostgresService;

namespace PassportsAPI.Quartz
{
    public class ImportJob : IJob
    {
        private readonly DataContext _context;
        private readonly ILogger<ImportJob> _logger;
        private readonly PostgresService _repository;

        public ImportJob(ILogger<ImportJob> logger, DataContext context, PostgresService repository)
        {
            _logger = logger;
            _context = context;
            _repository = repository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await _repository.BeginUpdateAsync();

                await _repository.UploadAsync();

                await _repository.EndUpdateAsync();


                _logger.LogInformation("Importing data finished ");
            }
            catch(Exception e)
            {
                _logger.LogError(e, "There was a mistake while imorting data");
            }
        }
    }
}
