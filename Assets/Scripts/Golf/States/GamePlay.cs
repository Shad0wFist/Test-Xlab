using System.Collections;
using Golf;
using UnityEngine;

namespace Golf
{
    public class GamePlay : MonoBehaviour
    {
        public Transform cameraTargetPos1, cameraTargetPos2;
        public float cameraMoveDuration = 2f;
        public Animator trollAnimator;
        public GameObject stoneSpawner, playerController;
        public TrollController trollController;
        public PlowController villagerPlow;

        private Camera mainCamera;

        private void OnEnable()
        {
            mainCamera = Camera.main;
            StartCoroutine(StartGameSequence());
        }

        private IEnumerator StartGameSequence()
        {

            // Шаг 1: Перемещение камеры к первой цели
            yield return MoveCamera(cameraTargetPos1, cameraMoveDuration);

            // Шаг 2: Запуск первой анимации тролля
            trollAnimator.SetTrigger("FirstTrigger");
            yield return new WaitForSeconds(trollAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Шаг 3: Перемещение камеры ко второй цели
            yield return MoveCamera(cameraTargetPos2, cameraMoveDuration);

            // Шаг 4: Запуск второй анимации тролля
            trollController.StartMoving();
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
    }
}
