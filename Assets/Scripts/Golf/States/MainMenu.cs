using UnityEngine;

namespace Golf
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject troll;
        [SerializeField] private GameObject gamePlayState;
        [SerializeField] private GameObject gameOverState;
        [SerializeField] private Transform mainMenuCamPos;
        [SerializeField] private GameObject gamePlayObjects;
        [SerializeField] private GameObject gameOverObjects;
        [SerializeField] private StoneSpawner stoneSpawner;

        private Camera mainCamera;

        private void OnEnable()
        {
            gamePlayObjects.SetActive(true);
            gameOverObjects.SetActive(false);
            mainCamera = Camera.main;
            mainCamera.transform.position = mainMenuCamPos.position;
            mainCamera.transform.rotation = mainMenuCamPos.rotation;

            troll.GetComponent<TrollController>().OnAwake();
            stoneSpawner.ClearStones();
            stoneSpawner.enabled = false;
            if (mainMenuUI != null)
                mainMenuUI.SetActive(true);

            
            if (gamePlayState != null)
                gamePlayState.SetActive(false);
            if (gameOverState != null)
                gameOverState.SetActive(false);
        }

        private void OnDisable()
        {
            if(mainMenuUI != null)
                mainMenuUI.SetActive(false);
        }

        public void Play()
        {
            gameObject.SetActive(false);
            if (gamePlayState != null)
                gamePlayState.SetActive(true);
        }
    }
}
