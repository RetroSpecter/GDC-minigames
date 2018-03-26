using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Represents the GUI for the OverallGame, displaying current round and
// lives as well as visual transitions between minigames
public class GameManagerGUI : MonoBehaviour {
    public GameObject livesCount;
    public GameObject roundCount;
    public GameObject headsupText;

    /// <summary>
    /// transition into the next minigame
    /// </summary>
    public virtual void transitionIntoGame() { }

    /// <summary>
    /// transition out of the current minigame
    /// </summary>
    public virtual void transitionOutGame() { }

    /// <summary>
    /// transition into next round
    /// </summary>
    public virtual void transitionIntoRound() { }

    /// <summary>
    /// transition into out of current round
    /// </summary>
    public virtual void transitionOutOfRound() { }

    /// <summary>
    /// updates the lives count to "value" in this.roundCount
    /// </summary>
    /// <param name="value"> the number of lives the lives GUI needs to be updated too</param>
    public virtual void updateLivesCount(int value) {
        //this assumes livesCount is a text field. Override to change this later
        livesCount.GetComponent<Text>().text = "Lives: " + value;
    }

    /// <summary>
    /// updates the lives count to "value" in this.roundCount
    /// </summary>
    /// <param name="value"> the current round GUI needs to be updated too</param>
    public virtual void updateRoundCount(int value) {
        //this assumes scoreCount is a text field. Override to change this later
        roundCount.GetComponent<Text>().text = "Round: " + value;
    }

    /// <summary>
    /// sends a message to the text display in the gui
    /// </summary>
    /// <param name="phrase">phrase we want the text display to read</param>
    public virtual void updateHeadsup(string phrase) {
        headsupText.GetComponent<Text>().text = phrase;
    }
}
