using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

// GameManager manages the whole state of the game. Keeps track of 
// lives and score, as well as loading up minigames.
public class GameManager : MonoBehaviour {


    public int lives = 3;
    private int numberOfMinigames = 1;
    public int round = 0;

    public static GameManager instance;
    public GameManagerGUI gui;

    [Header("Difficulty Settings")]
    public float mediumDifficulty;
    public float hardDifficulty;

    // Abstract Class for a game Manager gm:
    // gm.lives represents the player's current lives count
    // gm.numberOfMinigames represents the total number of minigames in the build
    // gm.round round represents the current round the player is at
    // gm.instance represents the singleton instance of GameManager
    // gm.gui represents the gui for the GameManager
    // gm.mediumDifficulty represents the cutoff for when minigames go from easy to medium difficulty
    // gm.hardDifficulty represent the cutoff for when minigames go from medium to hard difficulty

    // Rep Invariant:
    // gm.lives >= 0 &&
    // gm.numberOfMinigames > 1
    // gm.round >= 0
    // gm.instance != null
    // gm.gui != null
    // 0 <= gm.mediumDifficulty <= gm.hardDifficulty

    //debug makes it so you will always be playing the same minigame that is already loaded
    public bool debuggingLevel = true;
    
    private void Awake() {
        numberOfMinigames = SceneManager.sceneCountInBuildSettings - 1;
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this.gameObject);
        }
        gui.gameObject.SetActive(true);
        gui.updateRoundCount(round);
        gui.updateLivesCount(lives);


        if (!debuggingLevel) {
            loadNewMinigame();
        } else {
            StartCoroutine("startMinigame");
        }
        checkRep();
    }

    /// <summary>
    /// subtracts one from the total number of lives and updates it in the  GUI.
    /// </summary>
    public void loseLife() {
        checkRep();
        lives--;
        gui.updateLivesCount(lives);
        checkRep();
    }

    /// <summary>
    /// adds the parameter num to the total number of lives and updates it in the  GUI.
    /// </summary>
    /// <param name="num"> how many lives to add to the current count. </param>
    public void gainLife(int num) {
        checkRep();
        lives += num;
        gui.updateLivesCount(lives);
        checkRep();
    }

    /// <summary>
    /// Returns the current difficulty of the minigames based on the round
    /// </summary>
    /// <returns> the current difficulty of the minigames</returns>
    public difficulty getDifficulty() {
        checkRep();
        if (round > hardDifficulty) {
            return difficulty.HARD;
        } else if (round > mediumDifficulty) {
            return difficulty.MEDIUM;
        } else {
            return difficulty.EASY;
        }
    }

    /// <summary>
    /// ends a minigame, managing lives, and the new player state
    /// then loads up the next minigame
    /// </summary>
    /// <param name="win">whether or not the current minigame was won</param>
    public void bookendMinigame(bool win) {
        checkRep();
        if (win) { // sends a message on the GUI based of whether the player won
            gui.updateHeadsup("Nice One!");
        } else {
            gui.updateHeadsup("Oops");
            GameManager.instance.loseLife();
        }

        //gui.gameObject.SetActive(true);
        gui.transitionOutGame();

        if (lives == 0) { // manages if the player has lost 
            gui.updateHeadsup("Game Over");
        } else {
            Invoke("loadNewMinigame", 2); // loads up next game
        }
        checkRep();
    }

    /// <summary>
    /// loads the next minigame, increasing the round
    /// </summary>
    public void loadNewMinigame() {
        checkRep();
        round++;
        gui.updateHeadsup("");
        gui.transitionOutOfRound();
        if (debuggingLevel) {
            SceneManager.LoadScene(Application.loadedLevel); //I know this is kinda poopy...
        } else {
            int minigameIndex = Random.Range(1, numberOfMinigames);
            SceneManager.LoadScene(minigameIndex);
        }
        StartCoroutine("startMinigame");
        checkRep();
    }

    //TODO: Possibly move this into the GameManagerGUI for more flexibility
    private IEnumerator startMinigame() {
        yield return new WaitForSeconds(0.5f);

        gui.transitionIntoRound(); //tra
        gui.updateRoundCount(round);
        gui.updateHeadsup(FindObjectOfType<Minigame>().actionDescription);

        yield return new WaitForSeconds(1.5f);

        gui.transitionIntoGame();

        yield return new WaitForSeconds(0.25f);

        FindObjectOfType<Minigame>().minigameAwake(getDifficulty(), this);

        yield return new WaitForSeconds(0.25f);
        gui.updateHeadsup("");
    }

    /// <summary>
    /// checks to make sure rep invariant hold
    /// </summary>
    public void checkRep() {
        Assert.raiseExceptions = true;
        Assert.IsTrue(lives >= 0);
        Assert.IsTrue(numberOfMinigames > 1);
        Assert.IsTrue(round >= 0);
        Assert.IsTrue(instance != null);
        Assert.IsTrue(gui != null);
        Assert.IsTrue(mediumDifficulty >= 0);
    }
}

public enum difficulty { EASY, MEDIUM, HARD };