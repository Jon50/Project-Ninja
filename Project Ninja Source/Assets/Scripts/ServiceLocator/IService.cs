namespace TGM.FutureRacingGP.Locator
{
    public interface IService
    {
        void RegisterService(IService service, string tag = null);
        void UnregisterService();
    }
}