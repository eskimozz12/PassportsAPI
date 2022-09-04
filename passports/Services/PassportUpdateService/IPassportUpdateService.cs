namespace PassportsAPI.Services.PassportUpdateService
{
    public interface IPassportUpdateService
    {
        Task BeginUpdateAsync();
        Task UploadAsync();
        Task EndUpdateAsync();


    }
}
