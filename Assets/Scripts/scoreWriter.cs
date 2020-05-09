using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreWriter : MonoBehaviour
{
    public Text timeScore;
    public Text routerScore;
    public Text accuracyHeader;
    public Text accuracyScore;
    public Text difficultyScore;
    public Text victoryScore;
    public Text yourScore;
    private int playerScore;
    // Start is called before the first frame update

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /*
     * Represents a single High score entry
     * */
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }


    void Start()
    {
        playerScore = 0;
        timeScore.text = string.Format("+{0} * 10", PlayerPrefs.GetInt("timeLeft"));
        playerScore += PlayerPrefs.GetInt("timeLeft") * 10;
        routerScore.text = string.Format("+{0}", PlayerPrefs.GetInt("routerScore"));
        playerScore += PlayerPrefs.GetInt("routerScore");
        int accuracy = (int)(100 * PlayerPrefs.GetInt("shotsHit")) * (PlayerPrefs.GetInt("shotsFired"));
        accuracyHeader.text = string.Format("Accuracy Rating {0}:", accuracy);
        if(accuracy >= 100)
        {
            accuracyHeader.text = string.Format("Aimbotter +{0}", 2000);
            playerScore += 2000;
        }
        else if (accuracy >= 90)
        {
            accuracyHeader.text = string.Format("File Sniper +{0}", 1000);
            playerScore += 1000;
        }
        else if (accuracy >= 70)
        {
            accuracyHeader.text = string.Format("Data Marksman +{0}", 500);
            playerScore += 500;
        }
        else if (accuracy >= 50)
        {
            accuracyHeader.text = string.Format("Seek and Destroy +{0}", 250);
            playerScore += 250;
        }
        else if (accuracy >= 30)
        {
            accuracyHeader.text = string.Format("Eye on the Prize +{0}", 100);
            playerScore += 100;
        }
        else if (accuracy > 0)
        {
            accuracyHeader.text = string.Format("Spray and Pray +{0}", 0);
            playerScore += 0;
        }
        else
        {
            accuracyHeader.text = string.Format("Absolute Zero +{0}", 1);
            playerScore += 1;
        }

        if (PlayerPrefs.GetInt("completed") == 1)
        {
            victoryScore.text = string.Format("WIN +{0}", 1000);
            playerScore += 1000;
        }
        else
        {
            victoryScore.text = string.Format("LOSE +{0}", 0);
        }

        int difficultyLevel = PlayerPrefs.GetInt("Difficulty");
        switch (difficultyLevel)
        {
            default: difficultyScore.text = string.Format("Easy x{0}", 1); playerScore *= 1;  break;
            case 2: difficultyScore.text = string.Format("Intermediate x{0}", 5); playerScore *= 5; break;
            case 3: difficultyScore.text = string.Format("Hard x{0}", 10); playerScore *= 10; break;
        }
        yourScore.text = string.Format("{0}", playerScore);
    }

    public void onClick()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        int bottomScore = highscores.highscoreEntryList[highscores.highscoreEntryList.Count - 1].score;
        if((highscores.highscoreEntryList.Count - 1) < 10)
        {
            SceneManager.LoadScene("GameScene_InputWindow");
        }
        else if (playerScore > bottomScore)
        {
            SceneManager.LoadScene("GameScene_InputWindow");
        }
        else
        {
            SceneManager.LoadScene("GameScene_HighscoreTable");
        }
    }
}
