using System.Collections;
using Golf;
using UnityEngine;

namespace Golf
{
    public class GamePlay : MonoBehaviour
    {
        public GameObject gamePlayUI;
        public GameObject pauseState;
        public Transform cameraTargetPos1, cameraTargetPos2;
        public float cameraMoveDuration = 2f;
        public GameObject troll;
        private Animator trollAnimator;
        public GameObject stoneSpawner, playerController;
        public TrollController trollController;
        private PlowController villagerPlow;
        public GameObject plowObject;
        public GameObject gamePlayObjects;
        public GameObject gameOverObjects;
        private Quaternion plowRotation;
        [SerializeField] private AudioClip trollRoar;



        private Camera mainCamera;

        private void OnEnable()
        {
            SoundManager.Instance.PlayGameplayMusic();
            trollAnimator = troll.GetComponent<Animator>();
            plowRotation = plowObject.transform.rotation;
            mainCamera = Camera.main;
            gamePlayObjects.SetActive(true);
            gameOverObjects.SetActive(false);

            troll.GetComponent<TrollController>().OnAwake();
            stoneSpawner.GetComponent<StoneSpawner>().ClearStones();
            stoneSpawner.GetComponent<StoneSpawner>().enabled = false;
            villagerPlow = plowObject.GetComponent<PlowController>();
            StartCoroutine(StartGameSequence());
        }

        private void OnDisable()
        {
            if (gamePlayUI != null)
                gamePlayUI.SetActive(false);
            if (plowObject != null)
                plowObject.transform.rotation = plowRotation;
            if (villagerPlow != null)
                villagerPlow.enabled = false;
            if (playerController != null)
                playerController.SetActive(false);
        }

        private IEnumerator StartGameSequence()
        {
            gamePlayUI.SetActive(false);
            yield return MoveCamera(cameraTargetPos1, cameraMoveDuration);
            trollAnimator.SetTrigger("FirstTrigger");
            SoundManager.Instance.PlaySound(trollRoar);
            yield return new WaitForSeconds(trollAnimator.GetCurrentAnimatorStateInfo(0).length);
            yield return MoveCamera(cameraTargetPos2, cameraMoveDuration);
            trollController.StartMoving();
            gamePlayUI.SetActive(true);
            stoneSpawner.GetComponent<StoneSpawner>().enabled = true;
            villagerPlow.enabled = true;
            playerController.SetActive(true);
        }

        private IEnumerator MoveCamera(Transform target, float duration)
        {
            Vector3 startPosition = mainCamera.transform.position;
            Quaternion startRotation = mainCamera.transform.rotation;

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                mainCamera.transform.position = Vector3.Lerp(startPosition, target.position, elapsedTime / duration);
                mainCamera.transform.rotation = Quaternion.Lerp(startRotation, target.rotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            mainCamera.transform.position = target.position;
            mainCamera.transform.rotation = target.rotation;
        }

        public void Pause()
        {
            pauseState.SetActive(true);
        }
    }
}
