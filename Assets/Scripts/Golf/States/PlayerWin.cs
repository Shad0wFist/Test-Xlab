using UnityEngine;

namespace Golf
{
    public class PlayerWin : MonoBehaviour
    {
        [SerializeField] private GameObject playerWinUI;
        [SerializeField] private GameObject gamePlayState;
        [SerializeField] private GameObject mainMenuState;
        [SerializeField] private AudioClip winSound;

        private Camera mainCamera;

        private void OnEnable()
        {
            gamePlayState.SetActive(false);
            if (playerWinUI != null)
                playerWinUI.SetActive(true);
            SoundManager.Instance.PlaySound(winSound);
            SoundManager.Instance.GetCurrentPlayingMusic().volume = 0.2f;
        }

        private void OnDisable()
        {
            if(playerWinUI != null)
                playerWinUI.SetActive(false);
            SoundManager.Instance.GetCurrentPlayingMusic().volume = SoundManager.Instance.originalVolume;
        }

        
        public void Replay()
        {
            gameObject.SetActive(false);
            gamePlayState.SetActive(true);
        }
        public void MainMenu()
        {
            gameObject.SetActive(false);
            mainMenuState.SetActive(true);
        }
    }
}
