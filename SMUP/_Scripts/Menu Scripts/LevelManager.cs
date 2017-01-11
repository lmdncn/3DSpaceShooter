using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controls the level configurations menu and changes
/// </summary>
public class LevelManager : MonoBehaviour {

    public Toggle [] BActiveT;
    public Toggle[] SActiveT;
    public Toggle[] GActiveT;
    public InputField[] TScoreEnter; //0 bronze, 1 silver 2 gold
    public InputField[] TEnemiesEnter; //0 bronze, 1 silver 2 gold
    public Dropdown startLevel;

    // Use this for initialization
    void Start () {
        //make everything represent current values

        startLevel.captionText.text = "Start Level";

        for (int i = 0; i<5; i++)
        {
            BActiveT[i].isOn = Main.BEnemyOn[i];
            SActiveT[i].isOn = Main.SEnemyOn[i];
            GActiveT[i].isOn = Main.GEnemyOn[i];
        }
        TScoreEnter[0].text = Main.BTScore.ToString();
        TScoreEnter[1].text = Main.STScore.ToString();
        TScoreEnter[2].text = Main.GTScore.ToString();

        TEnemiesEnter[0].text = Main.BTEnemies.ToString();
        TEnemiesEnter[1].text = Main.STEnemies.ToString();
        TEnemiesEnter[2].text = Main.GTEnemies.ToString();
    }

    /// <summary>
    /// Which enemies are active, make the toggles represent that
    /// </summary>
    public void loadEnemiesActive()
    {
        for (int i = 0; i < 5; i++)
        {
            Main.BEnemyOn[i] = BActiveT[i].isOn;
            Main.SEnemyOn[i] = SActiveT[i].isOn;
            Main.GEnemyOn[i] = GActiveT[i].isOn;
        }

    }

    /// <summary>
    /// get the current set load score for each level, put it in feild
    /// </summary>
    public void loadWinScore()
    {
        Main.BTScore = int.Parse(TScoreEnter[0].text);
        Main.STScore = int.Parse(TScoreEnter[1].text);
        Main.GTScore = int.Parse(TScoreEnter[2].text);
    }

    /// <summary>
    /// get what number of enemies for each level
    /// </summary>
    public void loadEnemyNumber()
    {
        Main.BTEnemies = int.Parse(TEnemiesEnter[0].text);
        Main.STEnemies = int.Parse(TEnemiesEnter[1].text);
        Main.GTEnemies = int.Parse(TEnemiesEnter[2].text);

    }

    /// <summary>
    /// Start at this level
    /// </summary>
    public void setStartLevel()
    {
        Main.level = startLevel.value + 1;

    }
    
    /// <summary>
    /// Load it ALL (at start)
    /// </summary>
    public void loadAll()
    {
        loadEnemyNumber();
        loadWinScore();
        loadEnemiesActive();
        setStartLevel();
    }
}
