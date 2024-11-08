using UnityEngine;

namespace Golf
{
    public class PlayerWin : MonoBehaviour
    {
        [SerializeField] private GameObject playerWinUI;
        [SerializeField] private GameObject gamePlayState;
        [SerializeField] private GameObject mainMenuState;

        private Camera mainCamera;

        private void OnEnable()
        {
            gamePlayState.SetActive(false);
            if (playerWinUI != null)
                playerWinUI.SetActive(true);
        }

        private void OnDisable()
        {
            if(playerWinUI != null)
                playerWinUI.SetActive(false);
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
