namespace MusicPlatform.Services
{
    public interface IInitialDbService
    {
        event InitialDbService.ProgressUpdateHandler ProgressUpdated;

        Task InitializeDatabase(IProgress<int> progress);
    }
}