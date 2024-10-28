using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private GameObject cloud;
    [SerializeField] private float moveSpeed = 2f;
    private int currentCharacterIndex = 0;
    [SerializeField] private ParticleSystem rainPS;
    private Coroutine moveCoroutine;
    private float emoteDelay = 1.5f;

    public void CloudMove()
    {
        if (cloud != null)
        {
            if (rainPS.isPlaying)
            {
                rainPS.Stop();
            }

            // Меняем цель на следующего персонажа
            currentCharacterIndex = (currentCharacterIndex + 1) % characters.Count;

            // Если есть запущенная корутина, останавливаем её
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            // Запускаем новую корутину для перемещения к новому персонажу
            moveCoroutine = StartCoroutine(MoveToCharacter(characters[currentCharacterIndex]));
        }
    }

    private IEnumerator MoveToCharacter(GameObject target)
    {
        Vector3 startPos = cloud.transform.position;
        Vector3 targetPos = target.transform.position + new Vector3(0, 30, 0);

        // Начальный и конечный углы поворота
        float startYRotation = cloud.transform.eulerAngles.y;
        float endYRotation = Random.Range(0f, 360f);

        float journeyLength = Vector3.Distance(startPos, targetPos);
        float journeyTime = journeyLength / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < journeyTime)
        {
            elapsedTime += Time.deltaTime;
            float fraction = elapsedTime / journeyTime;

            cloud.transform.position = Vector3.Lerp(startPos, targetPos, fraction);

            float yRotation = Mathf.Lerp(startYRotation, endYRotation, fraction);
            cloud.transform.eulerAngles = new Vector3(cloud.transform.eulerAngles.x, yRotation, cloud.transform.eulerAngles.z);

            yield return null;
        }

        cloud.transform.position = targetPos;
        cloud.transform.eulerAngles = new Vector3(cloud.transform.eulerAngles.x, endYRotation, cloud.transform.eulerAngles.z);

        rainPS.Play();
        ParticleSystem characterPS = target.GetComponentInChildren<ParticleSystem>();
        yield return new WaitForSeconds(emoteDelay);
        if (characterPS != null)
        {
            characterPS.Play();
        }
    }
}
