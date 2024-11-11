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
        public AudioSource uiAudioSource; // Для звуков интерфейса
        public AudioSource generalAudioSource; // Для других звуков
        public AudioClip mainMenuClip;
        public AudioClip gameplayClip;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            if (mainMenuClip != null)
            {
                mainMenuClip.LoadAudioData(); // Загружаем данные аудио для главного меню
            }
            if (gameplayClip != null)
            {
                gameplayClip.LoadAudioData(); // Загружаем данные аудио для геймплея
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
            StartCoroutine(FadeMusic(from, to));
        }

        private IEnumerator FadeMusic(AudioSource from, AudioSource to)
        {
            float initialVolume = from.volume;

            // Плавное уменьшение громкости текущей музыки
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                from.volume = Mathf.Lerp(initialVolume, 0, t / fadeDuration);
                yield return null;
            }
            from.Stop();
            from.volume = initialVolume; // Восстанавливаем громкость для будущих запусков

            // Включаем следующую музыку с постепенным увеличением громкости
            to.volume = 0;
            to.Play();

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                to.volume = Mathf.Lerp(0, initialVolume, t / fadeDuration);
                yield return null;
            }
            to.volume = initialVolume;
        }

        // Проигрывает звук для UI или глобальных звуков
        public void PlaySound(AudioClip clip)
        {
            if (uiAudioSource != null && clip != null)
            {
                uiAudioSource.PlayOneShot(clip);
            }
        }

        // Проигрывает 3D-звук, если его вызывает другой объект
        public void Play3DSound(AudioClip clip, Vector3 position)
        {
            if (generalAudioSource != null && clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, position);  // Пример для 3D-звука
            }
        }
    }


}
