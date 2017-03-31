﻿using UnityEngine;
using System;



public class PauseGame : MonoBehaviour
{
    public Transform canvas;

    public static GM instance = null;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
              Pause();

            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;

            }
            else
            {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;

            }
        }
        
    }
    public void Pause()
    {

        
    }
}
