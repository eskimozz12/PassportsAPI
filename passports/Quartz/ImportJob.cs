using PassportsAPI.EfCore;
using Quartz;
using PassportsAPI.Services.PassportUpdateService;

namespace PassportsAPI.Quartz
{
    public class ImportJob : IJob
    {

        private readonly ILogger<ImportJob> _logger;
        private readonly IPassportUpdateService _repository;

        public ImportJob(ILogger<ImportJob> logger, IPassportUpdateService repository)
        {
            _logger = logger;
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
                _logger.LogError(e, "There was an error while importing data");
            }
        }
    }
}
