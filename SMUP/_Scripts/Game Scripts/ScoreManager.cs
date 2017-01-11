using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Manage the score of the shoot game
/// </summary>
public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	static public float playerScore;
    public int scoreToWin=10000;
   
    // Use this for initialization
    void Start () {
        scoreToWin = Main.maxScore;
        playerScore = 0;

	}

	// Update is called once per frame
	void Update () {
    scoreText.text = playerScore.ToString();
        if (playerScore >= scoreToWin) //the player wins!!
        {
            //You win this level

            UnityEngine.SceneManagement.SceneManager.LoadScene("WinLevel");
                Main.level++; // you win move to next level
        }
	}

    /// <summary>
    /// Add the score to main score save
    /// </summary>
    /// <param name="score"></param>
	static public void AddScore(int score){
        playerScore += score;
	}
}
//		public ScoreManager theScoreManager;
//		theScoreManager = FindObjectOfType<ScoreManager> ();
//		theScoreManager.AddScore(e.score);
//		theScoreManager.SetCountText ();
