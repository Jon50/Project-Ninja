using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TGM.FutureRacingGP.Locator;

using static TGM.FutureRacingGP.Static.ConstantValues;
using static TGM.FutureRacingGP.Save.SavingSystem;

namespace TGM.FutureRacingGP.Managers
{
    public class AudioManager : ServiceRegister<AudioManager>
    {
        [SerializeField] private bool _playFirstTrack = true;
        [SerializeField] private bool _testSoundFX = false;
        [SerializeField] private string _soundNameToTest;

        private AudioMixer _audioMixer;
        private List<SoundClass> _musicSounds;
        private List<SoundClass> _sfxSounds;

        private AudioSource _currentMusicPlaying;


        public void Initialize(List<SoundClass> musicSounds, List<SoundClass> sfxSounds, AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
            _musicSounds = musicSounds;
            _sfxSounds = sfxSounds;
        }


        private void Start() => StartCoroutine(DelaySoundStartup());


        private IEnumerator DelaySoundStartup()
        {
            Time.timeScale = 1;
            yield return new WaitForSeconds(1f);
            UpdateAudioSettings();

            if (_playFirstTrack)
                PlayMusic(_musicSounds[0].SoundName);

            if (_testSoundFX)
                PlaySFX(_soundNameToTest);
        }


        public AudioSource PlayMusic(string musicName)
        {
            if (_musicSounds.IsNull() || string.IsNullOrEmpty(musicName))
                return null;

            foreach (var music in _musicSounds)
            {
                if (music.SoundName == musicName && !music.audioSource.isPlaying)
                {
                    _currentMusicPlaying?.Stop();
                    _currentMusicPlaying = music.audioSource;
                    music.audioSource.Play();
                }
            }

            return _currentMusicPlaying;
        }


        public void PlaySFX(string sfxName)
        {
            if (_sfxSounds.IsNull() || string.IsNullOrEmpty(sfxName))
                return;

            foreach (var sfx in _sfxSounds)
            {
                if (sfx.SoundName == sfxName)
                    sfx.audioSource.PlayOneShot(sfx.AudioClip);
            }
        }


        public void PlaySFXAtPoint(string sfxName, Transform objRef)
        {
            if (_sfxSounds.IsNull() || string.IsNullOrEmpty(sfxName))
                return;

            foreach (var sfx in _sfxSounds)
            {
                if (sfx.SoundName == sfxName)
                {
                    if (!sfx.SpacialSources.ContainsKey((objRef, sfxName)))
                    {
                        var source = objRef.gameObject.AddComponent<AudioSource>();

                        source.clip = sfx.AudioClip;
                        source.volume = sfx.Volume;
                        source.pitch = sfx.Pitch;
                        source.loop = sfx.Loop;
                        source.playOnAwake = sfx.PlayOnAwake;
                        source.spatialBlend = sfx.SpacialBlend;
                        source.rolloffMode = sfx.RolloffMode;
                        source.minDistance = sfx.MinDistance;
                        source.maxDistance = sfx.MaxDistance;

                        sfx.SpacialSources.Add((objRef, sfxName), source);
                    }

                    sfx.SpacialSources[(objRef, sfxName)].Play();
                }
            }
        }


        public void UpdateAudioSettings()
        {
            _audioMixer.SetFloat(MUSIC_TOGGLE, LoadValue(MUSIC_SAVE_PATH, defaultValue: true) == true ? VOLUME_ON : VOLUME_OFF);
            _audioMixer.SetFloat(SFX_TOGGLE, LoadValue(SFX_SAVE_PATH, defaultValue: true) == true ? VOLUME_ON : VOLUME_OFF);
        }
    }
}