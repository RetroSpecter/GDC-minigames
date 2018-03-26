using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Minigame manages a minigame
// keeps track of the amount of time the player has and the state of the
// current minigame
public abstract class Minigame : MonoBehaviour {

    [Tooltip("current time till the minigame ends.")]
    public float time;
    [Tooltip("starting amount of time when it is medium difficulty")]
    public float mediumTime;
    [Tooltip("starting amount of time when it is hard difficulty")]
    public float hardTime;
    [Tooltip("the win/loss condition if the player runs the timer to zero")]
    public bool loseWhenNoTime = true;

    [Tooltip("the descrpition in the GUI when the player loads this minigame")]
    public string actionDescription = "Get Ready!";
    [Tooltip("holds all the gameObjects of the minigame.")]
    public GameObject minigameContainer;

    private bool gameActive = false;
    private GameManager gameManager;

    [Header("General MinigameGUI")]
    public TimerGUI timerGUI;

    // Abstract class for a minigame m
    // m.time represents the current time till the minigame is finisehd
    // m.mediumTime represents the amount of time the player has when the difficulty of the gameManager
    // is medium, m.hard time is likewise for the hard difficulty
    // m.loseWhenNoTime represents the win/loss condition if the player runs the timer to zero
    // m.actionDescription represents the descrpition in the GUI when the player loads this minigame
    // m.minigameContainer holds all the gameObjects of the minigame
    // m.gameActive represents if the minigame is currently playing or not
    // m.gameManager is the current gameManger of the game
    // m.timerGUI is the GUi for the countdown timer

    private void Start() {
        minigameContainer.SetActive(false);
    }

    public void Update() {
        if (gameActive) {
            timerCountdown();
            gameUpdate();
            timerGUI.updateTime(time);
        }
    }

/// <summary>
/// sets up the general initial state of the minigame
/// </summary>
/// <param name="difficulty">the difficulty of this game</param>
/// <param name="gm">the current game manager</param>
    public void minigameAwake(difficulty difficulty, GameManager gm) {
        gameManager = gm;
        if (difficulty == difficulty.MEDIUM) {
            time = mediumTime;
        } else if(difficulty == difficulty.HARD) {
            time = hardTime;
        }
        timerGUI.setFullTime(time);

        gameActive = true;
        minigameContainer.SetActive(true);
        gameStart(difficulty);
    }

    /// <summary>
    /// Setup for specific logic of this minigame
    /// </summary>
    public virtual void gameStart(difficulty difficulty) { }

    /// <summary>
    /// GameLogic for current minigame. Runs once every updateCall. 
    /// </summary>
    public virtual void gameUpdate() { }

    /// <summary>
    /// counts down the timer for the minigame
    /// if the timer becomes zero, it calls the correct win/loss condition
    /// based on loseWhenNoTime
    /// </summary>
    private void timerCountdown() {
        time -= Time.deltaTime;
        if (time <= 0) {
            if (loseWhenNoTime) {lose();} else {win();}
        }
    }

    /// <summary>
    /// Ends the minigame with a loss
    /// </summary>
    public virtual void lose() {
        print("lose");
        minigameContainer.SetActive(false);
        gameActive = false;
        gameManager.bookendMinigame(false);
    }

    /// <summary>
    /// Ends the minigame with a victuory
    /// </summary>
    public virtual void win() {
        print("win");
        minigameContainer.SetActive(false);
        gameManager.bookendMinigame(true);
        gameActive = false;
    }
}
