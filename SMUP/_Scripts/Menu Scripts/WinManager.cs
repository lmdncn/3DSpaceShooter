using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controls the shoot win screne
/// </summary>
public class WinManager : MonoBehaviour {

    public Text winText;
    public GameObject music1;
    public GameObject music2;
    GameObject winMusic;
    public Button again;

    // Use this for initialization
    void Start () {
        winText.text = "You Win Level " + (Main.level-1) + " !"; //yay you won

        if(Main.level == 4) //can't move up level if completed 3
        {
            again.interactable =false;
        }

        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Pause(); //stop the main game music

        //play the win music
        if (AudioControl.activeWin == 0)
        {
            winMusic = Instantiate(music1);
            winMusic.GetComponent<AudioSource>().Play();
        }
        if (AudioControl.activeWin == 1)
        {
            winMusic = Instantiate(music2);
            winMusic.GetComponent<AudioSource>().Play();
        }
    }
	

    public void resume()
    {
        Destroy(winMusic.gameObject, 1f); //stop win music
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play(); //bk to game music
        again.interactable = true;
    }
}
