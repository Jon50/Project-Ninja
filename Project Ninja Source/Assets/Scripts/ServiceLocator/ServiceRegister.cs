using UnityEngine;

namespace DefaultCompany.ProjectNinja.Locator
{
    public class ServiceRegister<T> : MonoBehaviour, IService
    {
        public virtual void Awake() => RegisterService(this);

        public virtual void OnDisable() => UnregisterService();

        public void RegisterService(IService service, string tag = null) => ServiceLocator.Register<IService>(service, tag);

        public void UnregisterService() => ServiceLocator.Unregister<T>();
    }
}