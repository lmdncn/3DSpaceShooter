using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The audio manager that sets user audio prefs in the configurations menu, including demos of some of them
/// </summary>
public class AudioControl : MonoBehaviour {

    public Dropdown musicSel;
    public Dropdown shootSel;
    public Dropdown destroySel;
    public Dropdown winMusicSel;
    public AudioSource[] audios;
    public Slider[] volSlider;
    public static int activeAudio;
    public static int activeFire;
    public static int activeWin;
    public static int activeDestroy;
    public GameObject bkgM;
    public GameObject bkgM1;
    public GameObject clickSound;
    public GameObject winS;
    public GameObject winS2;
    public GameObject shootS;
    public GameObject shootS2;
    public GameObject destroyS;
    public GameObject destroyS2;
    public static bool clicksound;

    public Toggle clickSoundT;

    bool afterStart;

    //so that the demos dont play from dropbox changing on awake
    public void Awake() {
        afterStart = false;
            }

    /// <summary>
    /// Make the dropboxes and slider represent the current configurations
    /// </summary>
    public void Start()
    {
        if (musicSel.value != activeAudio)
        { 
        musicSel.value = activeAudio;
        }
       
        winMusicSel.value = activeWin;

        if (shootSel.value != activeFire)
        {
            shootSel.value = activeFire;
        }

        if (destroySel.value != activeDestroy)
        {
            destroySel.value = activeDestroy;
        }

     volSlider[1].value = shootS.GetComponent<AudioSource>().volume ; //set valumes
     volSlider[0].value =  bkgM.GetComponent<AudioSource>().volume ;
     volSlider[2].value = winS.GetComponent<AudioSource>().volume ;

     GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = volSlider[0].value; 

     clickSoundT.isOn = clicksound;

     clicksound = clickSoundT.isOn;

        afterStart = true; // now demos can play
 
    }

    /// <summary>
    /// load the new music (stoping current playing)
    /// </summary>
    public void loadNewWinMusic()
    {
        activeWin = winMusicSel.value;
    }

    /// <summary>
    /// turns click sound on and off
    /// </summary>
    public void flipClick() {
        if (clicksound) { clicksound = false; }
        else { clicksound = true; }
    }

    /// <summary>
    /// changes the shoot sound 
    /// </summary>
    public void changeShootSound()
    {
        activeFire = shootSel.value;
        playShoot();

    }

    /// <summary>
    /// select the destroy sound
    /// </summary>
    public void changeDestroySound()
    {
        activeDestroy = destroySel.value;
        playDestroy();

    }

    /// <summary>
    /// load the new music (stoping current playing)
    /// </summary>
    public void loadNewMusic()
    {
        if (afterStart)
        {
            activeAudio = musicSel.value;
            GameObject.FindGameObjectWithTag("Music").GetComponent<BkMusic>().stopBKMusic();

            if (activeAudio == 0) { GameObject music = Instantiate(bkgM); music.GetComponent<AudioSource>().Play(); music.GetComponent<AudioSource>().volume = volSlider[0].value; }
            if (activeAudio == 1) { GameObject music1 = Instantiate(bkgM1); music1.GetComponent<AudioSource>().Play(); music1.GetComponent<AudioSource>().volume = volSlider[0].value; }
        }
    }

    /// <summary>
    /// Set the volume to slider
    /// </summary>
    public void changeMusicVolume()
    {
        bkgM.GetComponent<AudioSource>().volume = volSlider[0].value;
        bkgM1.GetComponent<AudioSource>().volume = volSlider[0].value;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = volSlider[0].value;
    }

    /// <summary>
    /// play shoot sound demo
    /// </summary>
    public void playShoot()
    {
        if (afterStart)
        {
            if (activeFire == 0) { GameObject.FindGameObjectWithTag("Shoot").GetComponent<AudioSource>().Play(); }
            if (activeFire == 1) { GameObject.FindGameObjectWithTag("Shoot2").GetComponent<AudioSource>().Play(); }
        }
    }

    /// <summary>
    /// play destroy sound demo
    /// </summary>
    public void playDestroy()
    {
        if (afterStart)
        {
            if (activeDestroy == 0) { GameObject.FindGameObjectWithTag("Destroy").GetComponent<AudioSource>().Play(); }
            if (activeDestroy == 1) { GameObject.FindGameObjectWithTag("Destroy2").GetComponent<AudioSource>().Play(); }
        }
    }

    /// <summary>
    /// Change the shoot and destroy sound volumes
    /// </summary>
    public void changeEffectsVolume()
    {
        shootS.GetComponent<AudioSource>().volume = volSlider[1].value;
        shootS2.GetComponent<AudioSource>().volume = volSlider[1].value;
        destroyS.GetComponent<AudioSource>().volume = volSlider[1].value;
        destroyS2.GetComponent<AudioSource>().volume = volSlider[1].value;
        GameObject.FindGameObjectWithTag("Shoot").GetComponent<AudioSource>().volume = volSlider[1].value;
        GameObject.FindGameObjectWithTag("Shoot2").GetComponent<AudioSource>().volume = volSlider[1].value;
        GameObject.FindGameObjectWithTag("Destroy").GetComponent<AudioSource>().volume = volSlider[1].value;
        GameObject.FindGameObjectWithTag("Destroy2").GetComponent<AudioSource>().volume = volSlider[1].value;

    }

    /// <summary>
    /// Change the win screne music volume
    /// </summary>
    public void changeWinVolume()
    {
        winS.GetComponent<AudioSource>().volume = volSlider[2].value;
        winS2.GetComponent<AudioSource>().volume = volSlider[2].value;
    }
}
