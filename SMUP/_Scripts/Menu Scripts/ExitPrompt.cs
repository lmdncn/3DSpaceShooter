using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Exit promt
/// </summary>
public class ExitPrompt : MonoBehaviour { //NOT used in project 3 (was used in 2)

    static Button[] toDisable;

    /// <summary>
    /// Yes actually quit
    /// </summary>
    public void quitYes()
    {
        Application.Quit();
    }

   /// <summary>
   /// I chicken out, dont quit, return to where i was
   /// </summary>
    public void quitNo()
    {
        Destroy(this.gameObject);
        for (int i = 0; i < toDisable.Length; i++)
        {
            toDisable[i].interactable = true;
        }
    }
    
    /// <summary>
    /// buttons bk to norm
    /// </summary>
    /// <param name="b"></param>
    static public void setButtons(Button [] b)
    {
        toDisable = b;
    }
}
