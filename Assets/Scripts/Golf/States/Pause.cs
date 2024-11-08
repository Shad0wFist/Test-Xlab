using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject PauseUI;
        [SerializeField] private GameObject gamePlayUI;
        [SerializeField] private GameObject gamePlayState;
        [SerializeField] private GameObject mainMenuState;

        private Camera mainCamera;

        private void OnEnable()
        {
            Time.timeScale = 0f;
            gamePlayUI.SetActive(false);
            if (PauseUI != null)
                PauseUI.SetActive(true);
            AudioListener.pause = false; // Отключаем остановку звука
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
            if(PauseUI != null)
                PauseUI.SetActive(false);
        }

        public void Continue()
        {
            gamePlayUI.SetActive(true);
            gameObject.SetActive(false);
        }
        public void Replay()
        {
            gamePlayState.SetActive(false);
            gamePlayState.SetActive(true);
            gameObject.SetActive(false);
        }
        public void MainMenu()
        {
            gameObject.SetActive(false);
            mainMenuState.SetActive(true);
        }
    }
}
