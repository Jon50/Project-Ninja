using System.Collections.Generic;
using UnityEngine;

using KadoNem.ProjectNinja.Locator;
using System.Linq;

namespace KadoNem.ProjectNinja.StateMachine
{
    public class StatePreparator : ServiceRegister<StatePreparator>
    {
        private List<MonoBehaviour> _components = new List<MonoBehaviour>();


        public void DisableComponents()
        {
            if (_components.Count == 0)
                _components = FindObjectsOfType<MonoBehaviour>().OfType<IStatePreparator>().Cast<MonoBehaviour>().ToList();

            foreach (var cmp in _components)
            {
                if (cmp.IsNull())
                    continue;

                if (cmp)
                    cmp.enabled = false;
            }
        }


        public void EnableComponents()
        {
            foreach (var cmp in _components)
            {
                if (cmp.IsNull())
                    continue;

                if (cmp)
                    cmp.enabled = true;
            }
        }
    }
}