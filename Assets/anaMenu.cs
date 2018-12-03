using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenu : MonoBehaviour {

    public Text highScoreText;
	void Start ()
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        highScoreText.text = "High Score=" + highScore;
	}
	
	
	void Update () {
		
	}
    public void Play()
    {
        SceneManager.LoadScene("level1");
    }
   public void Exit()
    {
        Application.Quit();
    }
}
