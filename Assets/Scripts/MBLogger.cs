using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBLogger : MonoBehaviour
{
    private void Awake()
    {
        Log("Awake");
    }

    private void OnEnable()
    {
        Log("Enable");
    }

    private void Start()
    {
        Log("Start");
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {

    }

    private void OnDisable()
    {
        Log("Disable");
    }

    private void OnDestroy()
    {
        Log("Destroy");
    }


    
    private void Log(string message)
    {
        Debug.Log($"{name}: message - frame{Time.frameCount}");
    }
}
