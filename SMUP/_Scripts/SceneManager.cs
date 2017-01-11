using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// For Basic Menu Scenes and Score Scenes like settings and win display...
/// </summary>
public class SceneManager : MonoBehaviour {

    string previousScene;
    string currentScene;
    public AudioSource clickSound;
    public GameObject exitPrompt;
    public Button [] buttons;
    public GameObject bkgM;
    public GameObject bkgM1;
    static bool startMusic = true;
    static bool setUpOnce = true;

    /// <summary>
    /// Set up Basic game, default setting of enemies, colors, score etc...
    /// </summary>
    void Awake()
    {
        previousScene = currentScene = "StartScene";
        if (bkgM != null && bkgM1 != null && startMusic)

        {
            GameObject music = Instantiate(bkgM);
        music.GetComponent<AudioSource>().Play();
            startMusic = false;
    }
        if (setUpOnce)
        {
            Main.level = 1;
            Main.bkSize = 9;
            Enemies.scoreSave0 = 100;
            Enemies.scoreSave1 = 200;
            Enemies.scoreSave2 = 250;
            Enemies.scoreSave3 = 300;
            Enemies.scoreSave4 = 400;
            AudioControl.clicksound = true;
            AudioControl.activeAudio = 0;
            AudioControl.activeFire = 0;
            AudioControl.activeWin = 0;
            AudioControl.activeDestroy = 0;

            for (int i = 0; i < 5; i++)
            {
                Main.enemyOn[i] = true;
            }
            Main.maxEnemies = 5;
            setUpOnce = false;

            Main.BTScore = 5000;
            Main.STScore = 8000;
            Main.GTScore = 10000;

            Main.BTEnemies = 10;
            Main.STEnemies = 15;
            Main.GTEnemies = 20;

            //Default enemies for each level
            Main.BEnemyOn[0] = true;
            Main.SEnemyOn[0] = true;
            Main.GEnemyOn[0] = true;
            Main.BEnemyOn[1] = true;
            Main.SEnemyOn[1] = true;
            Main.GEnemyOn[1] = true;
            Main.BEnemyOn[2] = false;
            Main.SEnemyOn[2] = true;
            Main.GEnemyOn[2] = true;
            Main.BEnemyOn[3] = false;
            Main.SEnemyOn[3] = false;
            Main.GEnemyOn[3] = true;
            Main.BEnemyOn[4] = false;
            Main.SEnemyOn[4] = true;
            Main.GEnemyOn[4] = false;
        }
    }

    /// <summary>
    /// Stop all the music playing with tag Music
    /// </summary>
    public void stopAllMusicCall()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Music");
        foreach (GameObject g in temp)
        {
            GameObject.Destroy(g.gameObject);
        }
    }

    /// <summary>
    /// Play the sound that is the click sound
    /// </summary>
    public void playClick() { if (AudioControl.clicksound) { clickSound.Play(); } }

    /// <summary>
    /// Pause the game
    /// </summary>
    public void pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resume the game
    /// </summary>
    public void unpause()
    {
        Time.timeScale = 1;
    }

    //Not Used Anymore
    /// <summary>
    /// call exit promt
    /// </summary>
    public void exit() { Instantiate(exitPrompt);
        ExitPrompt.setButtons(buttons);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

    /// <summary>
    /// Basic scene change
    /// </summary>
    /// <param name="nextScene"></param>
    public void changeScene(string nextScene)
    {
        previousScene = currentScene;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        currentScene = nextScene;
    }

    //Not Used Anymore
    /// <summary>
    /// Go back to previous scene
    /// </summary>
    public void goBack() {
        string temp = currentScene;
        UnityEngine.SceneManagement.SceneManager.LoadScene(previousScene);
        currentScene = previousScene;
        previousScene = temp;
    }

    /// <summary>
    /// Set the background
    /// </summary>
    /// <param name="bkgd"></param>
    public void setBkgd(int bkgd)
    {
        Main.bkg = bkgd;
    }

    /// <summary>
    /// Set to level 1
    /// </summary>
    public void resetToL1()
    {
        Main.level = 1;
    }

}


