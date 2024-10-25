using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private Transform m_point;
    [SerializeField] private GameObject[] m_fallingStonePrefabs;
    
    public float stoneLifeTime = 5f;
    public float spawnDelay = 1f;
    private float growDuration = 0.2f; // Продолжительность роста валуна
    private float shrinkDuration = 0.5f; // Продолжительность сжатия валуна

    
    private void Start()
    {
        if (m_point != null)
        {
            m_point = transform;
        }

    }
    IEnumerator Spawn()
    {
        int index = Random.Range(0, m_fallingStonePrefabs.Length);
        GameObject stone = Instantiate(m_fallingStonePrefabs[index], m_point.position, Random.rotation, transform);

        // Анимация появления (увеличение размеров от 0 до оригинального)
        Vector3 originalScale = stone.transform.localScale;
        stone.transform.localScale = Vector3.zero;
        for (float t = 0; t < growDuration; t += Time.deltaTime)
        {
            stone.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t / growDuration);
        }
        stone.transform.localScale = originalScale;

        yield return new WaitForSeconds(spawnDelay);

        StartCoroutine(DestroyStone(stone));
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
