using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGM.FutureRacingGP.Locator;


public class ActiveObjectsManager : ServiceRegister<ActiveObjectsManager>
{
    [System.Serializable]
    public class ActiveObjectsSettings
    {
        public string name;
        public float distanceFromTarget;
        public bool onlyFrustum;
        public bool useBounds;
        public List<Transform> objectsToDeactivate;

        /*[HideInInspector]*/
        public List<MeshRenderer> meshRenderers;
        /*[HideInInspector]*/
        public List<ParticleSystem> particles;
        /*[HideInInspector]*/
        public List<Vector3> distances;

        public void ClearLists()
        {
            meshRenderers.Clear();
            particles.Clear();
            distances.Clear();
        }
    }

    [SerializeField] private Camera _camera;
    [SerializeField, Range(0.1f, 1f)] private float _boundsSizeReduction;
    [SerializeField] private ActiveObjectsSettings[] _objectSettings;
    private Transform _target;


    public override void Awake()
    {
        base.Awake();

        for(int i = 0; i < _objectSettings.Length; i++)
        {
            var settings = _objectSettings[i];
            settings.ClearLists();

            for(int o = 0; o < settings.objectsToDeactivate.Count; o++)
            {
                var currentObject = settings.objectsToDeactivate[o];

                AddObjectAndRenderer(currentObject);

                void AddObjectAndRenderer( Transform currentParent )
                {
                    var hasRenderer = currentParent.GetComponent<MeshRenderer>();

                    if(!hasRenderer)
                        settings.objectsToDeactivate.Remove(currentParent);

                    for(int t = 0; t < currentParent.childCount; t++)
                    {
                        var child = currentParent.GetChild(t);
                        if(child.TryGetComponent<MeshRenderer>(out var meshRenderer))
                        {
                            if(settings.useBounds)
                                settings.distances.Add(meshRenderer.bounds.center);
                            else
                                settings.objectsToDeactivate.Add(meshRenderer.transform);

                            settings.meshRenderers.Add(meshRenderer);
                        }

                        AddObjectAndRenderer(child);
                    }
                }
            }
        }
    }


    private void Start()
    {
        // Needs a target injection, like a player
    }


    private void Update()
    {
        if(_objectSettings.Length <= 0)
            return;

        for(int i = 0; i < _objectSettings.Length; i++)
        {
            var settings = _objectSettings[i];

            for(int o = 0; o < settings.objectsToDeactivate.Count; o++)
            {
                var currentObject = settings.objectsToDeactivate[o];

                var distance = 0f;
                if(settings.useBounds)
                    distance = Vector3.Distance(settings.distances[o], _target.position);
                else
                    distance = Vector3.Distance(currentObject.position, _target.position);

                if(distance > settings.distanceFromTarget)
                {
                    MeshRenderer currentRenderer = null;
                    if(settings.meshRenderers.Count > 0)
                        currentRenderer = settings.meshRenderers[o];

                    ParticleSystem currentParticle = null;
                    if(settings.particles.Count > 0)
                        currentParticle = settings.particles[o];

                    if(currentRenderer != null)
                        currentRenderer.enabled = false;

                    else if(currentParticle != null && currentParticle.emission.enabled)
                    {
                        //currentParticle.Stop();
                        var emission = currentParticle.emission;
                        emission.enabled = false;
                    }
                }
                else if(settings.onlyFrustum)
                {
                    var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
                    Bounds bounds;
                    bool notInView = false;

                    MeshRenderer currentRenderer = null;
                    if(settings.meshRenderers.Count > 0)
                        currentRenderer = settings.meshRenderers[o];

                    ParticleSystem currentParticle = null;
                    if(settings.particles.Count > 0)
                        currentParticle = settings.particles[o];

                    if(currentRenderer != null)
                    {
                        bounds = new Bounds(currentRenderer.bounds.center, currentRenderer.bounds.size * _boundsSizeReduction);
                        notInView = !GeometryUtility.TestPlanesAABB(planes, bounds);
                    }

                    if(notInView)
                    {
                        if(currentRenderer != null)
                            currentRenderer.enabled = false;

                        else if(currentParticle != null && currentParticle.emission.enabled)
                        {
                            //currentParticle.Stop();
                            var emission = currentParticle.emission;
                            emission.enabled = false;
                        }
                    }
                }
                else
                {
                    MeshRenderer currentRenderer = null;
                    if(settings.meshRenderers.Count > 0)
                        currentRenderer = settings.meshRenderers[o];

                    ParticleSystem currentParticle = null;
                    if(settings.particles.Count > 0)
                        currentParticle = settings.particles[o];

                    if(currentRenderer != null)
                        currentRenderer.enabled = true;

                    else if(currentParticle != null && !currentParticle.emission.enabled)
                    {
                        //currentParticle.Play();
                        var emission = currentParticle.emission;
                        emission.enabled = true;
                    }
                }
            }
        }
    }

    public void AddObjects<T>( string listName, Transform obj, T type )
    {
        for(int i = 0; i < _objectSettings.Length; i++)
        {
            var settings = _objectSettings[i];

            if(settings.name != listName)
                continue;

            settings.objectsToDeactivate.Add(obj);

            if(typeof(T) == typeof(MeshRenderer))
                settings.meshRenderers.Add((MeshRenderer)(object)type);
            else if(typeof(T) == typeof(ParticleSystem))
                settings.particles.Add((ParticleSystem)(object)type);

            return;
        }

        Debug.LogWarning($"{nameof(ActiveObjectsManager)} :: List name <color=red> <<{listName}>> </color> no found!");
    }
}
