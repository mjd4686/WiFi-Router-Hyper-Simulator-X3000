﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    // Difficulty level setting
    private string[] difficulties ={"Easy", "Medium", "Hard"};
    private string[] tooltips = {"Timer set to 2 minutes, half health on all routers", "Timer set to 1 minute, standard health on all routers", "Timer set to 45 seconds, router health increased by 25%"};
    private string currentDifficulty;
    private int d = 0; // rotates through difficulties by array index
    public Text DifficultyLevel;
    public Text DifficultyTooltip;

    // Start is called before the first frame update
    void Start() {
        currentDifficulty = difficulties[d];
        DifficultyLevel.text = currentDifficulty;
        DifficultyTooltip.text = tooltips[d];
        PlayerPrefs.SetInt("shotsFired", 0);
        PlayerPrefs.SetInt("shotsHit", 0);
        PlayerPrefs.SetInt("routerScore", 0);
        PlayerPrefs.SetInt("timeLeft", 0);
        PlayerPrefs.SetInt("completed", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame() { // single scene mode
        SceneManager.LoadScene("LevelSelect");
    }

    public void PlayGameL1() {
        SceneManager.LoadScene("Level1");
    }

    public void PlayGameL2() {
        SceneManager.LoadScene("Level2");
    }
    
    public void Leaderboard() {
        SceneManager.LoadScene("GameScene_HighscoreTable");
    }
    
    public void Settings() {
        SceneManager.LoadScene("Settings");
    }
    
    public void Back() {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextTut() {
        SceneManager.LoadScene("Explainer2");
    }
    public void BackTut() {
        SceneManager.LoadScene("Explainer");
    }

    public void ChangeDifficulty() {
        if(d < 2) d++;
        else d -= 2;
        currentDifficulty = difficulties[d];

        DifficultyLevel.text = currentDifficulty;
        DifficultyTooltip.text = tooltips[d];

        PlayerPrefs.SetInt("Difficulty", d);
    }

    public void ResetLeaderboard() {
        PlayerPrefs.SetString("highscoreTable", null);
    }
}
