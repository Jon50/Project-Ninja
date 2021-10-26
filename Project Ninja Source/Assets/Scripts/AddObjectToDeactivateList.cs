using UnityEngine;
using TGM.FutureRacingGP.Locator;

public class AddObjectToDeactivateList : MonoBehaviour
{
    private enum TypesToAdd { MeshRenderer, ParticleSystem }

    [SerializeField] private TypesToAdd _types = TypesToAdd.MeshRenderer;
    [SerializeField] private string _listName;
    [SerializeField] private string _ignoreTag;

    private ActiveObjectsManager _activeObjectManager;

    private void Start()
    {
        if (!string.IsNullOrEmpty(_ignoreTag))
        {
            if (transform.root.CompareTag(_ignoreTag))
                return;
        }

        if (string.IsNullOrEmpty(_listName))
        {
            Debug.LogError($"{nameof(ActiveObjectsManager)} :: The listName is null or empty");
        }

        _activeObjectManager = ServiceLocator.Resolve<ActiveObjectsManager>();

        if (_activeObjectManager != null)
        {
            var particles = GetComponentsInChildren<ParticleSystem>(includeInactive: true);

            if (_types == TypesToAdd.ParticleSystem && particles.Length > 0)
            {
                for (int i = 0; i < particles.Length; i++)
                {
                    _activeObjectManager.AddObjects(_listName, particles[i].transform, particles[i]);
                }
            }

            var renderers = GetComponentsInChildren<MeshRenderer>(includeInactive: true);

            if (_types == TypesToAdd.MeshRenderer && renderers.Length > 0)
            {
                for (int i = 0; i < renderers.Length; i++)
                {
                    _activeObjectManager.AddObjects(_listName, renderers[i].transform, renderers[i]);
                }
            }
        }
    }
}
