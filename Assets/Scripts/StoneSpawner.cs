using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    private Transform m_point;
    [SerializeField] private GameObject[] m_fallingStonePrefabs;
    
    [SerializeField] private float stoneLifeTime = 5f;
    public float spawnDelay = 1f;
    private float m_growDuration = 0.2f; // Продолжительность роста валуна
    private float m_shrinkDuration = 0.5f; // Продолжительность сжатия валуна
    
    private bool m_canSpawn = true;

    
    private void Start()
    {
        m_point = transform;

    }
    
    public IEnumerator Spawn()
    {
        if (!m_canSpawn) yield break;
        m_canSpawn = false;

        int index = Random.Range(0, m_fallingStonePrefabs.Length);
        GameObject stone = Instantiate(m_fallingStonePrefabs[index], m_point.position, Random.rotation, transform);

        // Анимация появления (увеличение размеров от 0 до оригинального)
        Vector3 originalScale = stone.transform.localScale;
        stone.transform.localScale = Vector3.zero;
        for (float t = 0; t < m_growDuration; t += Time.deltaTime)
        {
            stone.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t / m_growDuration);
        }
        stone.transform.localScale = originalScale;

        StartCoroutine(DestroyStone(stone));

        yield return new WaitForSeconds(spawnDelay);
        m_canSpawn = true;
    }

    IEnumerator DestroyStone(GameObject stone)
    {
        yield return new WaitForSeconds(stoneLifeTime);

        // Анимация исчезновения (сжатие размеров до 0)
        Vector3 originalScale = stone.transform.localScale;
        for (float t = 0; t < m_shrinkDuration; t += Time.deltaTime)
        {
            stone.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t / m_shrinkDuration);
            yield return null;
        }
        Destroy(stone);
    }
    
}
