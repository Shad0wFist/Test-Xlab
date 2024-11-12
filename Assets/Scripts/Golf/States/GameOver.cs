using UnityEngine;

namespace Golf
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private GameObject gamePlayState;
        [SerializeField] private GameObject mainMenuState;
        [SerializeField] private AudioClip loseSound;

        private Camera mainCamera;

        private void OnEnable()
        {
            gamePlayState.SetActive(false);
            if (gameOverUI != null)
                gameOverUI.SetActive(true);
            SoundManager.Instance.PlaySound(loseSound);
            SoundManager.Instance.GetCurrentPlayingMusic().volume = 0.2f;
        }

        private void OnDisable()
        {
            if(gameOverUI != null)
                gameOverUI.SetActive(false);
            SoundManager.Instance.GetCurrentPlayingMusic().volume = SoundManager.Instance.originalVolume;
        }

        
        public void TryAgain()
        {
            gamePlayState.SetActive(true);
            gameObject.SetActive(false);
        }
        public void MainMenu()
        {
            mainMenuState.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
