using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    public List<GameObject> tools;  // Список инструментов-префабов
    public GameObject tool;  // Текущий инструмент
    public Transform rightHand;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeTool();
        }
    }

    void ChangeTool()
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
        // Vector3 targetPos = randomTool.transform.position;
        // Vector3 targetRot = randomTool.transform.rotation;
        tool = Instantiate(randomTool, rightHand);
    }
}
