using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame(){
        SceneManager.LoadScene("Level1");
    }
    
    public void Leaderboard(){
        SceneManager.LoadScene("GameScene_HighscoreTable");
    }
    
    public void Back(){
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeDifficulty(){
        
    }

    public void ResetLeaderboard(){
        
    }
}
