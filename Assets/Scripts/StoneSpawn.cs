using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawn : MonoBehaviour
{
    public GameObject fallingStonePrefab;
    public float spawnDelay = 1f;
    public float stoneLifeTime = 5f;
    
    private float growDuration = 0.2f; // Продолжительность роста валуна
    private float shrinkDuration = 0.5f; // Продолжительность сжатия валуна

    private bool canSpawn = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canSpawn)
        {
            StartCoroutine(SpawnStone());
        }
    }

    IEnumerator SpawnStone()
    {
        canSpawn = false;

        // Создаём валун в позиции StoneSpawn с рандомной Rotation
        GameObject stone = Instantiate(fallingStonePrefab, transform.position, Random.rotation, transform);

        // Анимация появления (увеличение размеров от 0 до оригинального)
        Vector3 originalScale = stone.transform.localScale;
        stone.transform.localScale = Vector3.zero;
        for (float t = 0; t < growDuration; t += Time.deltaTime)
        {
            stone.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t / growDuration);
            yield return null;
        }
        stone.transform.localScale = originalScale;

        StartCoroutine(DestroyStone(stone));

        yield return new WaitForSeconds(spawnDelay);

        canSpawn = true;
    }

    IEnumerator DestroyStone(GameObject stone)
    {
        yield return new WaitForSeconds(stoneLifeTime);

        // Анимация исчезновения (сжатие размеров до 0)
        Vector3 originalScale = stone.transform.localScale;
        for (float t = 0; t < shrinkDuration; t += Time.deltaTime)
        {
            stone.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t / shrinkDuration);
            yield return null;
        }
        Destroy(stone);
    }
}