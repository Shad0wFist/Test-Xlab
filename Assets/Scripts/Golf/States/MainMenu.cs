using UnityEngine;

namespace Golf
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuUI;
        [SerializeField] private GameObject troll;
        [SerializeField] private GamePlay gamePlayState;
        [SerializeField] private Transform mainMenuCamPos;
        [SerializeField] private Transform trollPos;
        [SerializeField] private StoneSpawner stoneSpawner;

        private Camera mainCamera;

        private void OnEnable()
        {
            mainCamera = Camera.main;
            mainCamera.transform.position = mainMenuCamPos.position;
            mainCamera.transform.rotation = mainMenuCamPos.rotation;

            troll.transform.position = trollPos.position;
            troll.GetComponent<TrollController>().Awake();
            stoneSpawner.ClearStones();
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
