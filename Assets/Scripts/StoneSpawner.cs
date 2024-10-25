using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private Transform m_point;
    [SerializeField] private GameObject[] m_fallingStonePrefabs;
    
    private void Start()
    {
        if (m_point != null)
        {
            m_point = transform;
        }

    }
    public void Spawn()
    {
        int index = Random.Range(0, m_fallingStonePrefabs.Length);
        Instantiate(m_fallingStonePrefabs[index], m_point.position, m_point.rotation);
    }
    
}
