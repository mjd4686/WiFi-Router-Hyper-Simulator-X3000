﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            returnToMain();
        }
    }

    public void returnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
