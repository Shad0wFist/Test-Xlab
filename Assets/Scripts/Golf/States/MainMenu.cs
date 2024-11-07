using UnityEngine;

namespace Golf
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GamePlay gamePlayState;
        [SerializeField] private Transform mainMenuCamPos;

        private Camera mainCamera;

        private void OnEnable()
        {
            mainCamera = Camera.main;
            mainCamera.transform.position = mainMenuCamPos.position;
            mainCamera.transform.rotation = mainMenuCamPos.rotation;
            if (mainMenuUI != null)
                mainMenuUI.SetActive(true);
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
                gamePlayState.gameObject.SetActive(true);
        }
    }
}
