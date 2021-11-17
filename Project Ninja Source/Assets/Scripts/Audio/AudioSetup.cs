using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace KadoNem.ProjectNinja.Managers
{
    [RequireComponent(typeof(AudioManager))]
    public class AudioSetup : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private List<SoundClass> _musicSounds = new List<SoundClass>();
        [SerializeField] private List<SoundClass> _sfxSounds = new List<SoundClass>();

        public AudioMixer AudioMixer => _audioMixer;

        private void Awake()
        {
            foreach (var music in _musicSounds)
            {
                music.audioSource = gameObject.AddComponent<AudioSource>();
                var refAudioSource = music.audioSource;

                if (_audioMixer)
                    refAudioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Music")[0];

                refAudioSource.clip = music.AudioClip;
                refAudioSource.volume = music.Volume;
                refAudioSource.pitch = music.Pitch;
                refAudioSource.loop = music.Loop;
                refAudioSource.playOnAwake = music.PlayOnAwake;
            }

            foreach (var sfx in _sfxSounds)
            {
                sfx.audioSource = gameObject.AddComponent<AudioSource>();
                var refAudioSource = sfx.audioSource;

                if (_audioMixer)
                    refAudioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SFX")[0];

                refAudioSource.clip = sfx.AudioClip;
                refAudioSource.volume = sfx.Volume;
                refAudioSource.pitch = sfx.Pitch;
                refAudioSource.loop = sfx.Loop;
                refAudioSource.playOnAwake = sfx.PlayOnAwake;
                refAudioSource.spatialBlend = sfx.SpacialBlend;
                refAudioSource.rolloffMode = sfx.RolloffMode;
                refAudioSource.minDistance = sfx.MinDistance;
                refAudioSource.maxDistance = sfx.MaxDistance;

                sfx.SetDefaults();
            }

            if (transform.TryGetComponent<AudioManager>(out var manager))
            {
                manager.Initialize(_musicSounds, _sfxSounds, _audioMixer);
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning("No Audio Manager Found!");
            }
#endif
        }
    }
}