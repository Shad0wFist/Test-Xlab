using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    [SerializeField] private List<GameObject> tools;  // Список инструментов-префабов
    [SerializeField] private GameObject tool;  // Текущий инструмент
    [SerializeField] private Transform rightHand;

    public void ChangeTool()
    {
        if (rightHand == null)
        {
            Debug.LogWarning("Right hand is not assigned!");
            return;
        }

        if (tool != null)
        {
            Destroy(tool);
        }

        // Выбираем случайный инструмент из списка и создаём его
        GameObject randomTool = tools[Random.Range(0, tools.Count)];
        tool = Instantiate(randomTool, rightHand);
    }
}
