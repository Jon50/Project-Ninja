namespace DefaultCompany.ProjectNinja.Locator
{
    public interface IService
    {
        void RegisterService(IService service, string tag = null);
        void UnregisterService();
    }
}