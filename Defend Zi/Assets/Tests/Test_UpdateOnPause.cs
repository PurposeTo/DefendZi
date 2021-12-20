using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_UpdateOnPause : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
        Debug.Log("Awake call");
    }

    private void Update()
    {
        Debug.Log("Update call");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate call");
    }
}
