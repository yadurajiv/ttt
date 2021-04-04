using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {


    // NEW - static variables for ai, ties and your score
    public static int aiScore;
    public static int playerScore;
    public static int draws;

    // UI Text for displaying scores
    public Text yourScore;
    public Text aiScoreText;
    public Text gameTies;

    // checking if this instance is already active
    private static ScoreKeeper playerInstance;

    void Awake() {

        // If its the first time we are being created, stay alive, else destory the newly created copy
        if (playerInstance == null) {
            DontDestroyOnLoad(this);
            playerInstance = this;
        } else {
            Destroy(gameObject);
        }
    }


    // Update the scores
    private void Update() {

        // if references to the text boxes are missing re-connect with them before accessing.

        if (yourScore == null) {
            yourScore = GameObject.Find("yourScore").GetComponent<Text>();
        }

        if (aiScoreText == null) {
            aiScoreText = GameObject.Find("aiScore").GetComponent<Text>();
        }

        if (gameTies == null) {
            gameTies = GameObject.Find("gameTies").GetComponent<Text>();
        }

        yourScore.text = "You: " + playerScore;
        aiScoreText.text = "AI: " + aiScore;
        gameTies.text = "Tied: " + draws;
    }
}
