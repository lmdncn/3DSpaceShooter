using UnityEngine;
using System.Collections;

/// <summary>
/// Control the ingame shound for shoot and destroy
/// </summary>
public class GSoundRunner : MonoBehaviour {
    public GameObject S1;
    public GameObject S2;
    public GameObject D1;
    public GameObject D2;


    /// <summary>
    /// Load the appropriate sounds based on configurations  to user value
    /// </summary>
    void Awake()
    {
        GameObject.FindGameObjectWithTag("Shoot").GetComponent<AudioSource>().volume = S1.GetComponent<AudioSource>().volume; //set volume
        GameObject.FindGameObjectWithTag("Shoot2").GetComponent<AudioSource>().volume = S2.GetComponent<AudioSource>().volume;//set volume

        GameObject.FindGameObjectWithTag("Destroy").GetComponent<AudioSource>().volume = D1.GetComponent<AudioSource>().volume;//set volume
        GameObject.FindGameObjectWithTag("Destroy2").GetComponent<AudioSource>().volume = D2.GetComponent<AudioSource>().volume;//set volume
    }

}
