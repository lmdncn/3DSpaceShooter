using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Makes the background fit screne when inst
/// </summary>
public class BkgSizer : MonoBehaviour {

    public Slider sizer;

	// Use this for initialization
	void Start () {
        sizer.value = Main.bkSize;
    }
    
    /// <summary>
    /// set its size
    /// </summary>
    public void setBkgSize()
    {
        Main.bkSize = sizer.value;
    }

    //thats all it does folks
}
