using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Golf
{
    public class StoneSpawner : MonoBehaviour
    {
        private Transform m_point;
        private List<Stone> m_stones = new List<Stone>();
        [SerializeField] private GameObject[] fallingStonePrefabs;

        [SerializeField] private float spawnDelay = 1f;
        private float m_growDuration = 0.2f; // Продолжительность роста валуна

        private bool m_canSpawn = true;

        private void Start()
        {
            m_point = transform;
        }

        private void Update()
        {
            StartCoroutine(Spawn());
        }

        public void ClearStones()
        {
            foreach (var stone in m_stones)
            {
                Destroy(stone.gameObject);
            }

            m_stones.Clear();
        }

        public IEnumerator Spawn()
        {
            if (!m_canSpawn) yield break;
            m_canSpawn = false;

            int index = Random.Range(0, fallingStonePrefabs.Length);
            GameObject stoneObject = Instantiate(fallingStonePrefabs[index], m_point.position, Random.rotation, transform);

            // Получаем компонент Stone и запускаем анимацию появления
            Stone stone = stoneObject.GetComponent<Stone>();
            m_stones.Add(stone);
            if (stone != null)
            {
                stone.StartGrowing(m_growDuration);
                stone.StartCoroutine(stone.DestroyAfterTime()); // Начинаем уничтожение камня по времени
            }

            yield return new WaitForSeconds(spawnDelay);
            m_canSpawn = true;
        }
    }
}
