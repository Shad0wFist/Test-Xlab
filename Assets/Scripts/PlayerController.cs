using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FreeCamera freeCamera;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject stoneSpawner;
    [SerializeField] private GameObject cloudController;
    [SerializeField] private List<GameObject> villagers;


    void Update()
    {
        if (ui.activeSelf)
        {
            return;
        }

        if (freeCamera != null)
        {
            freeCamera.Move();
        }

        if (Input.GetKeyDown(KeyCode.X) && stoneSpawner != null)
        {
            StartCoroutine(stoneSpawner.GetComponent<StoneSpawner>().Spawn());
        }

        if (Input.GetKeyDown(KeyCode.Z) && cloudController != null)
        {
            cloudController.GetComponent<CloudController>().CloudMove();
        }

        if (Input.GetKeyDown(KeyCode.Space) && villagers != null)
        {
            for (int i = 0; i < villagers.Count; i++)
            {
                villagers[i].GetComponent<ToolController>().ChangeTool();
            }
        }
    }
}
