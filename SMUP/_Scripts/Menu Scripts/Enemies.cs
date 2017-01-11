using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controls the enemies configurations menu
/// </summary>
public class Enemies : MonoBehaviour {

    static int e;
    public InputField scoreIn;
    public Dropdown eSelect;
    public Dropdown cSelect;
    public Color[] cOptions;
    public Enemy[] enemies;

    public static int scoreSave0;
    public static int scoreSave1;
    public static int scoreSave2;
    public static int scoreSave3;
    public static int scoreSave4;

    // Use this for initialization
    void Start()
    {
        e = 5;
        eSelect.captionText.text = "Select Enemy...";//set all the text to select____ etc.
        scoreIn.text = "Enter Score...";
        cSelect.captionText.text = "Select Color...";
        cSelect.itemText.text = "select Color...";
        eSelect.value = 0;
        setSelectedE();
        
    }

    /// <summary>
    /// set the selected enemy from drop down,
    /// updating the other feilds to that enemies current settings
    /// </summary>
    public void setSelectedE()
    {
        e = eSelect.value;
        switch (e)
        {
            case 0:
                scoreIn.text = scoreSave0.ToString();
                break;
            case 1:
                scoreIn.text = scoreSave1.ToString();
                break;
            case 2:
                scoreIn.text = scoreSave2.ToString();
                break;
            case 3:
                scoreIn.text = scoreSave3.ToString();
                break;
            case 4:
                scoreIn.text = scoreSave4.ToString();
                break;

        }
        setColorBox(e);
        
    }

    /// <summary>
    /// set the color box to current selected enemy color representation
    /// </summary>
    /// <param name="e"></param>
    void setColorBox(int e)
    {
        enemies[e].getColor(0); //get the color
        int colorValue = new int() ;

        for (int i = 0; i < cOptions.Length; i++)
        {
            if (cOptions[i] == enemies[e].getColor(0))
            {
                colorValue = i; //which color from list (get the int value)
                break;
            }
        }
        cSelect.value = colorValue;
    }

    /// <summary>
    /// set selected enemy color to color chosen from drop down menu
    /// </summary>
    public void setEnemyColor()
    {
        Color c = cOptions[cSelect.value];
        enemies[e].setColor(c, 0);

    }

    /// <summary>
    /// can't click any othe buttons
    /// </summary>
    public void enableUIs()
    {
        eSelect.interactable = false;
        cSelect.interactable = false;
        scoreIn.interactable = false;
    }

    /// <summary>
    /// set the selected enemies score value from input feild
    /// </summary>
    public void setEnemyScoreValue()
    {
        int Score = int.Parse(scoreIn.textComponent.text);
        enemies[e].setScore(Score);
        Debug.Log("Score set as:" + enemies[e].getScore());

        switch (e)
        {
            case 0: scoreSave0 = Score;
                break;
            case 1:
                scoreSave1 = Score;
                break;
            case 2:
                scoreSave2 = Score;
                break;
            case 3:
                scoreSave3 = Score;
                break;
            case 4:
                scoreSave4 = Score;
                break;
        }     
    }

    /// <summary>
    /// Get the sekected enemies current score value
    /// </summary>
    /// <param name="eNum"></param>
    /// <returns></returns>
    public static int getScoresave(int eNum)
    {
        switch (eNum)
        {
            case 0:
                return scoreSave0;
            case 1:
                return scoreSave1;
            case 2:
                return scoreSave2;
            case 3:
                return scoreSave3;
            case 4:
                return scoreSave4;
            default:
                return 100;
        }
    }

}
