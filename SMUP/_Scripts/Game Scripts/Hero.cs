using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the Hero ship (The player)
/// </summary>
public class Hero : MonoBehaviour
{
    static public Hero S; // Singleton
                          // These fields control the movement of the ship
    public bool BonusScore = true;
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    // Ship status information
    [SerializeField]
    private float _shieldLevel = 1; // Add the underscore!

    // Weapon fields
    public Weapon[] weapons;

    public bool ____________________________;
    public Bounds bounds;

    public float gameRestartDelay = 2f;

    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    // Create a WeaponFireDelegate field named fireDelegate.
    public WeaponFireDelegate fireDelegate;

    void Awake()
    {
        S = this; // Set the Singleton 

        bounds = Utils.CombineBoundsOfChildren(this.gameObject);

    }

    /// <summary>
    /// Set the hero ship up
    /// </summary>
    void Start()
    {

        // Reset the weapons to start _Hero with 1 blaster
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);
    }

    /// <summary>
    /// Move the hero around by user input, collisions, shooting etc...
    /// </summary>
    void Update()
    {
        //	Pull	in	information	from	the	Input	class								
        float xAxis = Input.GetAxis("Horizontal");                                						
        float yAxis = Input.GetAxis("Vertical");                                                                                               				
                                                                                                                                                                
        //	Change	transform.position	based	on	the	axes								
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
       
        //	Rotate	the	ship	to	make	it	feel	more	dynamic							
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
       
        //This is beter then bounds, because it uses the player edges, so aspect etc. can get changed during gameplay
        var left = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        var right = Camera.main.ViewportToWorldPoint(Vector3.one).x;
        var top = Camera.main.ViewportToWorldPoint(Vector3.one).y;
        var bottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y;

        //lets stop at the edges
        if (pos.x < left) // if its at the left edge
        {
            pos.x = left; // dont u dare go father left u little ship 
        }
        else if (pos.x > right) // if at right edge
        {
            pos.x = right; // dont u dare go father right u little ship 
        }
        else {
            if (pos.y < bottom) // at bottom edge
            {
                pos.y = bottom; // dont u dare go father down u little ship
            }
            else if (pos.y > top) // at top edge
            {
                pos.y = top; // dont u dare go father left u little ship
            }
        }

        transform.position = pos;

        bounds.center = transform.position;

        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        { fireDelegate();
            

        }



    }

    // This variable holds a reference to the last triggering GameObject
    public GameObject lastTriggerGo = null;

    /// <summary>
    /// Collisions
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        print("Triggered: " + other.gameObject.name);

        GameObject go = Utils.FindTaggedParent(other.gameObject);
        // If there is a parent with a tag
        if (go != null)
        {
            if (go != null)
            {
                // Make sure it's not the same triggering go as last time
                if (go == lastTriggerGo)
                { // 2
                    return;
                }
                lastTriggerGo = go; // 3
                if (go.tag == "Enemy")
                {
                    // If the shield was triggered by an enemy
                    // Decrease the level of the shield by 1
                    shieldLevel--;
                    // Destroy the enemy
                    Main.enemiesOnScreen--;
                    Destroy(go); // 4
                    if (AudioControl.activeDestroy == 0)
                    {
                        GameObject.FindGameObjectWithTag("Destroy").GetComponent<AudioSource>().Play();
                    }
                    if (AudioControl.activeDestroy == 1)
                    {
                        GameObject.FindGameObjectWithTag("Destroy2").GetComponent<AudioSource>().Play();
                    } //and make noise
                }
                else if (go.tag == "PowerUp")
                {
                    // If the shield was triggerd by a PowerUp
                    AbsorbPowerUp(go);
                }
                else if (go.tag == "ProjectileEnemy")
                {
                    Debug.Log("Hero Hit");
                    if (AudioControl.activeDestroy == 0)
                    {
                        GameObject.FindGameObjectWithTag("Destroy").GetComponent<AudioSource>().Play();
                    }
                    if (AudioControl.activeDestroy == 1)
                    {
                        GameObject.FindGameObjectWithTag("Destroy2").GetComponent<AudioSource>().Play();
                    }
                    shieldLevel--;
                    Destroy(other);
                }
                else {
                    print("Triggered: " + go.name);
                }
            }
        }
    }

    /// <summary>
    /// the lives of the player
    /// </summary>
    public float shieldLevel
    {
        get
        {
            return (_shieldLevel); // 1
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4); // 2
                                                // If the shield is going to be set to less than zero
            if (value < 0)
            { // 3
                if (AudioControl.activeDestroy == 0)
                {
                    GameObject.FindGameObjectWithTag("Destroy").GetComponent<AudioSource>().Play();
                }
                if (AudioControl.activeDestroy == 1)
                {
                    GameObject.FindGameObjectWithTag("Destroy2").GetComponent<AudioSource>().Play();
                } //and make noise
                Destroy(this.gameObject);

                // Tell Main.S to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

    /// <summary>
    /// Power hero up
    /// </summary>
    /// <param name="go"></param>
    public void AbsorbPowerUp( GameObject go ) {

    PowerUp pu = go.GetComponent<PowerUp>();

        if (pu.type == WeaponType.shield) // If it's the shield
            {
            if (shieldLevel == 4) {
                if (BonusScore)
                {
                    ScoreManager.AddScore(1000); //Your ships already OP, 10 points for Giffyndor!!
                }
            }

            shieldLevel++; //increase the sheild
        } 

        //After step 2 updates the ship is now OP enough to carry both types of weapons!!!!!
        //So its a shooting weapon
        Weapon w = GetWeaponSlotOfType(WeaponType.none); // Find an available weapon slot

        if (pu.type == WeaponType.blaster) { //if blaster picked up
            if (w != null) //not full
            {
                w.SetType(WeaponType.blaster);
            }
            else
            {
                if (BonusScore)
                {
                    ScoreManager.AddScore(250); //ship pretty OP, doesnt need more blasters, 10 points for Hufflepuff!!
                }
            }
        }

        if (pu.type == WeaponType.spread) { //if spread picked upt

            if (w != null) // if not full add spread to empty slot
            {
                w.SetType(WeaponType.spread);
            }
            else //Its full, but we might be able to improve!
            {
                w= GetWeaponSlotOfType(WeaponType.blaster);

                if (w != null) { w.SetType(WeaponType.spread); } //if theres a blaster, lets make it a spread
                else {
                    if (BonusScore)
                    {
                        ScoreManager.AddScore(500); //your ship is full OP, bonus points
                    }
                }
            }
        }
 
pu.AbsorbedBy( this.gameObject ); // make the powerup disapear
}

/// Return MAtching Slot of Type arg, Null if none
Weapon GetWeaponSlotOfType (WeaponType searchFor)
    {
    for (int i=0; i<weapons.Length; i++)
        {
        if ( weapons[i].type == searchFor )
            {return( weapons[i] );} //Return the open slot
        }
    return( null );
    }
    
/// Get Rid of all the weapons
void ClearWeapons()
    {
    foreach (Weapon w in weapons)
        {
        w.SetType(WeaponType.none);
        }
    }


}


