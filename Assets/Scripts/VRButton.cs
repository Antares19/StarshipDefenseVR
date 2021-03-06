﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VRButton : MonoBehaviour
{
    public UnityEvent OnPress;
    public float MinPressInterval = 0.5f;

    private float lastPressed = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController")
        {
            if (Time.realtimeSinceStartup - lastPressed >= MinPressInterval)
            {
                lastPressed = Time.realtimeSinceStartup;
                OnPress.Invoke();
                //Debug.Log("Button WOrks");
                
                

                Scene thisScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(thisScene.name);
            }
        }
    }
}
