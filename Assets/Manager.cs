using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    // our grid of strings
    private string a1 = "-";
    private string a2 = "-";
    private string a3 = "-";
    private string b1 = "-";
    private string b2 = "-";
    private string b3 = "-";
    private string c1 = "-";
    private string c2 = "-";
    private string c3 = "-";

    // NEW - a list of strings
    public List<string> slots = new List<string>();

    // an int that designates the current player
    private int currentPlayer;

    // is the game over or not
    private bool isGameOver;

    // an object to show when the game is over
    public GameObject gameOverDisplay;

    // our scorekeeper
    public ScoreKeeper scoreKeeper;

    private void Start() {

        // is the game over or not
        isGameOver = false;

        // hide the game over display just in case
        gameOverDisplay.SetActive(false);

        // NEW - init the slots list
        slots = new List<string>();

        // NEW - add slot strings to our list of slots
        slots.Add("a1");
        slots.Add("a2");
        slots.Add("a3");
        slots.Add("b1");
        slots.Add("b2");
        slots.Add("b3");
        slots.Add("c1");
        slots.Add("c2");
        slots.Add("c3");

        // set current player to 1
        currentPlayer = 1;

    }

    // handles button click from user
    public void ButtonPressed(string buttonName) {
        // exit this function if the game is over or if its not our turn
        if(isGameOver || currentPlayer != 1) {
            return;
        }

        // play on a particular slot
        PlayOnSlot(buttonName);
    }

    // plays an X or an O on a given slot for the current player
    void PlayOnSlot(string buttonName) {
        if(isGameOver) {
            return;
        }

        // NEW - finding the button that was clicked using its name!
        var btn = GameObject.Find(buttonName);

        // NEW - we use the GetComponent function to get the actual button component
        var thisButton = btn.GetComponent<Button>();
        
        // NEW - we use the GetComponentInChildren function to get the UI Text component in the child UI Text object
        var buttonText = btn.GetComponentInChildren<Text>();
        
        // we disable the button
        thisButton.interactable = false;

        // change the button text to X
        buttonText.text = (currentPlayer == 1) ? "X" : "O";

        // NEW - remove the played slot from the slots
        slots.Remove(buttonName);

        // update our game grid of strings
        switch (buttonName) {
            case "a1":
                a1 = (currentPlayer == 1) ? "X" : "O";
                break;
            case "a2":
                a2 = (currentPlayer == 1) ? "X" : "O";
                break;
            case "a3":
                a3 = (currentPlayer == 1) ? "X" : "O";
                break;
            case "b1":
                b1 = (currentPlayer == 1) ? "X" : "O";
                break;
            case "b2":
                b2 = (currentPlayer == 1) ? "X" : "O";
                break;
            case "b3":
                b3 = (currentPlayer == 1) ? "X" : "O";
                break;
            case "c1":
                c1 = (currentPlayer == 1) ? "X" : "O";
                break;
            case "c2":
                c2 = (currentPlayer == 1) ? "X" : "O";
                break;
            case "c3":
                c3 = (currentPlayer == 1) ? "X" : "O";
                break;
        }

        // check if anyone won, tied or not
        var winner = CheckForWinner();

        // if player 1 was the winner
        if (winner == 1) {
            ScoreKeeper.playerScore++;
            gameOverDisplay.SetActive(true);
            gameOverDisplay.GetComponentInChildren<Text>().text = "Game Over - You Win!";

            // if player 2 was the winner
        } else if (winner == 2) {

            ScoreKeeper.aiScore++;
            gameOverDisplay.SetActive(true);
            gameOverDisplay.GetComponentInChildren<Text>().text = "Game Over - You Lose!";

            // if the game was a tie
        } else if(winner == 3) {
            ScoreKeeper.draws++;
            gameOverDisplay.SetActive(true);
            gameOverDisplay.GetComponentInChildren<Text>().text = "Game Over - Its a Tie!";

            // no one won
        } else { 

            // End turn for current player
            if(currentPlayer == 1) {
                currentPlayer = 2;

                // NEW - Invokes the ComputerMoves function after 1 second
                Invoke("ComputerMoves", 0f);
            } else {
                currentPlayer = 1;
            }
        }

    }

    // a function to check for a winner
    private int CheckForWinner() {

        // debugging the board
        // Debug.Log(a1 + a2 + a3 + "\n" + b1 + b2 + b3 + "\n" + c1 + c2 + c3 + "\n");

        // we assume its gameover till its not
        isGameOver = true;

        // our win patterns to check against XXX and OOO
        var p1Pattern = "XXX";
        var p2Pattern = "OOO";

        // checking if X - player 1 won
        if ((a1 + a2 + a3) == p1Pattern) { // horizontal
            return 1;
        } else if ((b1 + b2 + b3) == p1Pattern) { // horizontal
            return 1;
        } else if ((c1 + c2 + c3) == p1Pattern) { // horizontal
            return 1;
        } else if ((a1 + b1 + c1) == p1Pattern) { // vertical
            return 1;
        } else if ((a2 + b2 + c2) == p1Pattern) { // vertical
            return 1;
        } else if ((a3 + b3 + c3) == p1Pattern) { // vertical
            return 1;
        } else if ((a1 + b2 + c3) == p1Pattern) { // X from left
            return 1;
        } else if ((a3 + b2 + c1) == p1Pattern) { // X from right
            return 1;
        }

        // checking if O - player 2 won
        if ((a1 + a2 + a3) == p2Pattern) { // horizontal
            return 2;
        } else if ((b1 + b2 + b3) == p2Pattern) { // horizontal
            return 2;
        } else if ((c1 + c2 + c3) == p2Pattern) { // horizontal
            return 2;
        } else if ((a1 + b1 + c1) == p2Pattern) { // vertical
            return 2;
        } else if ((a2 + b2 + c2) == p2Pattern) { // vertical
            return 2;
        } else if ((a3 + b3 + c3) == p2Pattern) { // vertical
            return 2;
        } else if ((a1 + b2 + c3) == p2Pattern) { // X from left
            return 2;
        } else if ((a3 + b2 + c1) == p2Pattern) { // X from right
            return 2;
        }

        // check to see if all slots have been filled!!
        if(a1 != "-" && a2 != "-" && a3 != "-" && b1 != "-" && b2 != "-" && b3 != "-" && c1 != "-" && c2 != "-" && c3 != "-") {
            return 3;
        }

        // we nobody won or wasn't a true, I guess its not game over yet!
        isGameOver = false;

        // no body won this turn!!
        return 0;
    }

    // not the smartest AI, but he will do
    void ComputerMoves() {

        // our win patterns to check against XXX and OOO
        var p2PatternA = "-OO";
        var p2PatternB = "O-O";
        var p2PatternC = "OO-";

        // check if I can win, move to win
        if ((a1 + a2 + a3) == p2PatternA || (a1 + a2 + a3) == p2PatternB || (a1 + a2 + a3) == p2PatternC) { // horizontal
            if (a1 == "-") {
                PlayOnSlot("a1");
                return;
            } else if (a2 == "-") {
                PlayOnSlot("a2");
                return;
            } else if (a3 == "-") {
                PlayOnSlot("a3");
                return;
            }
        } else if ((b1 + b2 + b3) == p2PatternA || (b1 + b2 + b3) == p2PatternB || (b1 + b2 + b3) == p2PatternC) { // horizontal
            if (b1 == "-") {
                PlayOnSlot("b1");
                return;
            } else if (b2 == "-") {
                PlayOnSlot("b2");
                return;
            } else if (b3 == "-") {
                PlayOnSlot("b3");
                return;
            }
        } else if ((c1 + c2 + c3) == p2PatternA || (c1 + c2 + c3) == p2PatternB || (c1 + c2 + c3) == p2PatternC) { // horizontal
            if (c1 == "-") {
                PlayOnSlot("c1");
                return;
            } else if (c2 == "-") {
                PlayOnSlot("c2");
                return;
            } else if (c3 == "-") {
                PlayOnSlot("c3");
                return;
            }
        } else if ((a1 + b1 + c1) == p2PatternA || (a1 + b1 + c1) == p2PatternB || (a1 + b1 + c1) == p2PatternC) { // horizontal
            if (a1 == "-") {
                PlayOnSlot("a1");
                return;
            } else if (b1 == "-") {
                PlayOnSlot("b1");
                return;
            } else if (c1 == "-") {
                PlayOnSlot("c1");
                return;
            }
        } else if ((a2 + b2 + c2) == p2PatternA || (a2 + b2 + c2) == p2PatternB || (a2 + b2 + c2) == p2PatternC) { // horizontal
            if (a2 == "-") {
                PlayOnSlot("a2");
                return;
            } else if (b2 == "-") {
                PlayOnSlot("b2");
                return;
            } else if (c2 == "-") {
                PlayOnSlot("c2");
                return;
            }
        } else if ((a3 + b3 + c3) == p2PatternA || (a3 + b3 + c3) == p2PatternB || (a3 + b3 + c3) == p2PatternC) { // horizontal
            if (a3 == "-") {
                PlayOnSlot("a3");
                return;
            } else if (b3 == "-") {
                PlayOnSlot("b3");
                return;
            } else if (c3 == "-") {
                PlayOnSlot("c3");
                return;
            }
        } else if ((a1 + b2 + c3) == p2PatternA || (a1 + b2 + c3) == p2PatternB || (a1 + b2 + c3) == p2PatternC) { // horizontal
            if (a1 == "-") {
                PlayOnSlot("a1");
                return;
            } else if (b2 == "-") {
                PlayOnSlot("b2");
                return;
            } else if (c3 == "-") {
                PlayOnSlot("c3");
                return;
            }
        } else if ((a3 + b2 + c1) == p2PatternA || (a3 + b2 + c1) == p2PatternB || (a3 + b2 + c1) == p2PatternC) { // horizontal
            if (a3 == "-") {
                PlayOnSlot("a3");
                return;
            } else if (b2 == "-") {
                PlayOnSlot("b2");
                return;
            } else if (c1 == "-") {
                PlayOnSlot("c1");
                return;
            }
        }

        // check if player can win, block that possibility
        var p1PatternA = "-XX";
        var p1PatternB = "X-X";
        var p1PatternC = "XX-";

        // check if I can win, move to win
        if ((a1 + a2 + a3) == p1PatternA || (a1 + a2 + a3) == p1PatternB || (a1 + a2 + a3) == p1PatternC) { // horizontal
            if (a1 == "-") {
                PlayOnSlot("a1");
                return;
            } else if (a2 == "-") {
                PlayOnSlot("a2");
                return;
            } else if (a3 == "-") {
                PlayOnSlot("a3");
                return;
            }
        } else if ((b1 + b2 + b3) == p1PatternA || (b1 + b2 + b3) == p1PatternB || (b1 + b2 + b3) == p1PatternC) { // horizontal
            if (b1 == "-") {
                PlayOnSlot("b1");
                return;
            } else if (b2 == "-") {
                PlayOnSlot("b2");
                return;
            } else if (b3 == "-") {
                PlayOnSlot("b3");
                return;
            }
        } else if ((c1 + c2 + c3) == p1PatternA || (c1 + c2 + c3) == p1PatternB || (c1 + c2 + c3) == p1PatternC) { // horizontal
            if (c1 == "-") {
                PlayOnSlot("c1");
                return;
            } else if (c2 == "-") {
                PlayOnSlot("c2");
                return;
            } else if (c3 == "-") {
                PlayOnSlot("c3");
                return;
            }
        } else if ((a1 + b1 + c1) == p1PatternA || (a1 + b1 + c1) == p1PatternB || (a1 + b1 + c1) == p1PatternC) { // horizontal
            if (a1 == "-") {
                PlayOnSlot("a1");
                return;
            } else if (b1 == "-") {
                PlayOnSlot("b1");
                return;
            } else if (c1 == "-") {
                PlayOnSlot("c1");
                return;
            }
        } else if ((a2 + b2 + c2) == p1PatternA || (a2 + b2 + c2) == p1PatternB || (a2 + b2 + c2) == p1PatternC) { // horizontal
            if (a2 == "-") {
                PlayOnSlot("a2");
                return;
            } else if (b2 == "-") {
                PlayOnSlot("b2");
                return;
            } else if (c2 == "-") {
                PlayOnSlot("c2");
                return;
            }
        } else if ((a3 + b3 + c3) == p1PatternA || (a3 + b3 + c3) == p1PatternB || (a3 + b3 + c3) == p1PatternC) { // horizontal
            if (a3 == "-") {
                PlayOnSlot("a3");
                return;
            } else if (b3 == "-") {
                PlayOnSlot("b3");
                return;
            } else if (c3 == "-") {
                PlayOnSlot("c3");
                return;
            }
        } else if ((a1 + b2 + c3) == p1PatternA || (a1 + b2 + c3) == p1PatternB || (a1 + b2 + c3) == p1PatternC) { // horizontal
            if (a1 == "-") {
                PlayOnSlot("a1");
                return;
            } else if (b2 == "-") {
                PlayOnSlot("b2");
                return;
            } else if (c3 == "-") {
                PlayOnSlot("c3");
                return;
            }
        } else if ((a3 + b2 + c1) == p1PatternA || (a3 + b2 + c1) == p1PatternB || (a3 + b2 + c1) == p1PatternC) { // horizontal
            if (a3 == "-") {
                PlayOnSlot("a3");
                return;
            } else if (b2 == "-") {
                PlayOnSlot("b2");
                return;
            } else if (c1 == "-") {
                PlayOnSlot("c1");
                return;
            }
        }

        // if the center slot is availalble - play b2
        if (b2 == "-") {
            PlayOnSlot("b2");
            return;
        }

        // can you trap the player?

        // if there is nothing smart to do, then play random
        // pick a random slot from available slots
        var pickIndex = Random.Range(0, slots.Count - 1);

        // pick name of the slot
        var pick = slots[pickIndex];

        // play the slot
        PlayOnSlot(pick);

    }

    // Restart the game
    public void RestartGame() {
        SceneManager.LoadScene(0);
    }

}
