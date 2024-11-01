using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    [SerializeField] private List<GameObject> villagers;

    
    public void ChangeTools()
    {
        if (villagers != null)
            foreach (var villager in villagers)
            {
                villager.GetComponent<ToolChanger>().ChangeTool();
            }
    }
}
