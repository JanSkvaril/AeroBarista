namespace AeroBarista.Services.Interfaces
{
    public interface IBrewProcessService
    {
        public void RegisterTickCallback(Action<TimeSpan> callback);

        public void Start();
        public void Stop();
    }
}