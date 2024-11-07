using UnityEngine;

namespace Golf
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverUI;

        private Camera mainCamera;

        private void OnEnable()
        {
            if (gameOverUI != null)
                gameOverUI.SetActive(true);
        }

        private void OnDisable()
        {
            if(gameOverUI != null)
                gameOverUI.SetActive(false);
        }

        
        public void Replay()
        {
            gameObject.SetActive(false);
        }
        public void MainMenu()
        {
            gameObject.SetActive(false);
        }
    }
}
