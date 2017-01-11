using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the Projectile fires from hero or enemy
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private WeaponType _type;
    // This public property masks the field _type & takes action when it is set
    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }

    /// <summary>
    /// Sets un offscreen check
    /// </summary>
    void Awake()
    {
        // Test to see whether this has passed off screen every 2 seconds
        InvokeRepeating("CheckOffscreen", 2f, 2f);
    }

    /// <summary>
    /// Sets the type of projectile
    /// </summary>
    /// <param name="eType"></param>
    public void SetType(WeaponType eType)
    {
        // Set the _type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        ((GetComponent<Renderer>()).material).color = def.projectileColor;
    }

    /// <summary>
    /// Is it off screen?
    /// </summary>
    void CheckOffscreen()
    {
        if (this.transform.position.y > Camera.main.ViewportToWorldPoint(Vector3.one).y) //better catch for off screen
        {
            Destroy(this.gameObject);
        }

        if (this.transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y) //better catch for off screen
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Loads the audio
    /// </summary>
    void Start()
    {
        if (AudioControl.activeFire == 0)
        {
            GameObject.FindGameObjectWithTag("Shoot").GetComponent<AudioSource>().Play();
        }
        if (AudioControl.activeFire == 1)
        {
            GameObject.FindGameObjectWithTag("Shoot2").GetComponent<AudioSource>().Play();
        }
    }
    }
