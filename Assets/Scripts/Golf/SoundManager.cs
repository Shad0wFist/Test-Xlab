using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [Header("Background Music")]
        public AudioSource mainMenuMusic;
        public AudioSource gameplayMusic;
        public float fadeDuration = 1f;

        [Header("Sound Effects")]
        public AudioSource generalAudioSource; // Для других звуков

        [Header("Audio Clips")]
        public List<AudioClip> audioClips; // Список всех аудиоклипов, которые нужно предзагрузить

        // Поле для хранения исходной громкости
        public float originalVolume = 0.5f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                foreach (var clip in audioClips)
                {
                    clip.LoadAudioData();
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void PlayGameplayMusic()
        {
            if (!gameplayMusic.isPlaying) // Проверяем, не запущен ли уже трек
            {
                SwitchMusic(mainMenuMusic, gameplayMusic);
            }
        }

        public void PlayMainMenuMusic()
        {
            if (!mainMenuMusic.isPlaying) // Проверяем, не запущен ли уже трек
            {
                SwitchMusic(gameplayMusic, mainMenuMusic);
            }
        }

        // Плавный переход между треками
        public void SwitchMusic(AudioSource from, AudioSource to)
        {
            if (!from.isPlaying && to.isPlaying) return; // Если целевой трек уже играет, не делаем ничего
            StartCoroutine(FadeSwitchMusic(from, to));
        }

        private IEnumerator FadeSwitchMusic(AudioSource from, AudioSource to)
        {
            // Плавное уменьшение громкости текущей музыки
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                from.volume = Mathf.Lerp(originalVolume, 0, t / fadeDuration);
                yield return null;
            }
            from.Stop();
            from.volume = originalVolume; // Восстанавливаем громкость для будущих запусков

            // Включаем следующую музыку с постепенным увеличением громкости
            to.volume = 0;
            to.Play();

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                to.volume = Mathf.Lerp(0, originalVolume, t / fadeDuration);
                yield return null;
            }
            to.volume = originalVolume;
        }

        public IEnumerator FadeMusic(float targetVolume)
        {
            AudioSource audioSource = GetCurrentPlayingMusic();
            // Сохраняем начальную громкость, если это еще не сделано
            originalVolume = audioSource.volume;

            // Плавное уменьшение громкости текущей музыки
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(originalVolume, targetVolume, t / fadeDuration);
                yield return null;
            }

            audioSource.volume = 0.25f; // Устанавливаем конечную громкость
        }

        public IEnumerator RestoreMusicVolume()
        {
            AudioSource audioSource = GetCurrentPlayingMusic();
            // Плавное увеличение громкости до исходного значения
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, originalVolume, t / fadeDuration);
                yield return null;
            }

            audioSource.volume = originalVolume; // Восстанавливаем исходную громкость
        }

        public AudioSource GetCurrentPlayingMusic()
        {
            if (mainMenuMusic.isPlaying) return mainMenuMusic;
            if (gameplayMusic.isPlaying) return gameplayMusic;
            return null; // Возвращаем null, если никакой трек не играет
        }


        public void PlaySound(AudioClip clip)
        {
            if (generalAudioSource != null && clip != null)
            {
                generalAudioSource.PlayOneShot(clip);
            }
        }
    }


}
