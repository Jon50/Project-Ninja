using System.Collections.Generic;
using UnityEngine;

namespace KadoNem.ProjectNinja.Managers
{
    [CreateAssetMenu(menuName = "TGM-Glow-Car-Racing/New Sound")]
    public class SoundClass : ScriptableObject
    {
        [SerializeField] private string _soundName;
        [SerializeField] private AudioClip _audioClip;
        [Range(0, 1)] [SerializeField] private float _volume = 0.2f;
        [Range(-2, 3)] [SerializeField] private float _pitch = 1f;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _playOnAwake;

        [Header("3D Sound Settings")]
        [Range(0, 1)] [SerializeField] private float _spacialBlend;
        [SerializeField] private AudioRolloffMode _rolloffMode;
        [SerializeField] private float _minDistance = 1f;
        [SerializeField] private float _maxDistance = 5f;

        [HideInInspector] public AudioSource audioSource;
        private Dictionary<(Transform Reference, string SFXName), AudioSource> _spacialSources = new Dictionary<(Transform Reference, string SFXName), AudioSource>();

        public string SoundName => _soundName;
        public AudioClip AudioClip => _audioClip;
        public float Volume => _volume;
        public float Pitch => _pitch;
        public bool Loop => _loop;
        public bool PlayOnAwake => _playOnAwake;
        public float SpacialBlend => _spacialBlend;
        public AudioRolloffMode RolloffMode => _rolloffMode;
        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;
        public Dictionary<(Transform Reference, string SFXName), AudioSource> SpacialSources => _spacialSources;

        public void SetDefaults()
        {
            _spacialSources = new Dictionary<(Transform Reference, string SFXName), AudioSource>();
        }
    }
}