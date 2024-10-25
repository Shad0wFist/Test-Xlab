using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FreeCamera freeCamera;
    [SerializeField] private GameObject ui;
    void Update()
    {
        if (!ui.activeSelf)
        {
            return;
        }

        if (freeCamera != null)
        {
            freeCamera.Move();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(StoneSpawner.StartCoroutine());
        }

        

    }

    // private void MoveCamera()
    // {
    //     float h = Input.GetAxis("Horizontal");
	// 	float v = Input.GetAxis("Vertical");
    // }
}
