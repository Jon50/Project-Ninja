using System.Collections.Generic;

namespace TGM.FutureRacingGP.Locator
{
    public static class ServiceLocator
    {
        private static List<IService> _services = new List<IService>();
        private static List<(string tag, IService service)> _taggedServices = new List<(string, IService)>();

        public static void Register<T>(IService service, string tag = null, bool isSingle = false) where T : IService
        {
            if (tag != null)
            {
                _taggedServices.Add((tag, service));
                return;
            }

            if (isSingle)
                for (int i = 0; i < _services.Count; i++)
                {
                    if (_services[i].GetType() == typeof(T))
                    {
                        _services[i] = service;
                        return;
                    }
                }

            _services.Add(service);
        }

        public static void Unregister<T>()
        {
            for (int i = 0; i < _services.Count; i++)
            {
                if (_services[i].GetType() == typeof(T))
                    _services.RemoveAt(i);
            }

            for (int i = 0; i < _taggedServices.Count; i++)
            {
                if (_taggedServices[i].service.GetType() == typeof(T))
                    _taggedServices.RemoveAt(i);
            }
        }

        public static T Resolve<T>(string tag = null)
        {
            IService _service = default;

            if (tag != null && _taggedServices.Exists(x => x.tag == tag))
            {
                _service = _taggedServices.Find(x => x.tag == tag).service;
                return (T)_service;
            }

            foreach (var service in _services)
            {
                if (service.GetType() == typeof(T))
                {
                    _service = service;
                    break;
                }
            }

            return (T)_service;
        }

        public static List<T> ResolveList<T>(string tag = null)
        {
            var returnList = new List<T>();

            if (tag != null && _taggedServices.Exists(x => x.tag == tag))
            {
                foreach (var taggedService in _taggedServices)
                    if (taggedService.tag == tag)
                        returnList.Add((T)taggedService.service);

                return returnList;
            }

            foreach (var service in _services)
                if (service.GetType() == typeof(T))
                    returnList.Add((T)service);

            return returnList;
        }
    }
}