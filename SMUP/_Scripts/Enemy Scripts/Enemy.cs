using UnityEngine;
using System.Collections;

/// <summary>
/// The basic Enemy control class
/// </summary>
public class Enemy : MonoBehaviour
{
    public GameObject destroySound;

    public float speed = 10f; // The speed in m/s
    public float fireRate = 0.3f; // Seconds/shot (Unused)
    public float health = 10;
    int score = new int(); // Points earned for destroying this
    public bool alive; //Stops multiple Power Up Drops
    public int showDamageForFrames = 2; // # frames to show damage
    public float powerUpDropChance = 1f; // Chance to drop a power-up
    public bool ________________;
    public int type;
    public Color[] originalColors;
    public Material[] materials;// All the Materials of this & its children
    public int remainingDamageFrames = 0; // Damage frames left

    public Bounds bounds; // The Bounds of this and its children
    public Vector3 boundsCenterOffset; // Dist of bounds.center from position

    /// <summary>
    /// Starts the enemy on its way to kill you
    /// </summary>
    void Awake()
    {
        alive = true; // Its alliiiiivvveeee!
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }

        InvokeRepeating("CheckOffscreen", 0f, 2f);
    }

    //sets up the score value of the enemy
    void Start()
    {
        type = 0;
        setScore(Enemies.getScoresave(type));
    }

    /// <summary>
    /// sets the score value of the enemy
    /// </summary>
    /// <param name="s"></param>
    public void setScore( int s) { score = s;
        Debug.Log("In the score" + score);
    }

    /// <summary>
    /// return the score value the type of enemy is worth
    /// </summary>
    /// <returns></returns>
    public int getScore() { return score; }
    
    /// <summary>
    /// makes the enemy that color
    /// </summary>
    /// <param name="C"></param>
    /// <param name="matNum"></param>
    public void setColor(Color C, int matNum) {
        materials[matNum].SetColor("_Color", C);
        Debug.Log("Enemy color set");
    }

    /// <summary>
    /// get what color the enemy is
    /// </summary>
    /// <param name="matNum"></param>
    /// <returns></returns>
    public Color getColor(int matNum)
    {
        return materials[matNum].color;
    }

    // Update is called once per frame
    void Update()
    {        
        Move(); //move the enemy a bit every frame

        if (remainingDamageFrames > 0)
        {
            remainingDamageFrames--;
            if (remainingDamageFrames == 0)
            {
                UnShowDamage();
            }
        }

    }

    /// <summary>
    /// the movement function
    /// </summary>
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    // This is a Property: A method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    /// <summary>
    /// determines if the enemy left the sreen
    /// </summary>
    void CheckOffscreen()
    {
        // If bounds are still their default value...
        if (bounds.size == Vector3.zero)
        {
            // then set them
            bounds = Utils.CombineBoundsOfChildren(this.gameObject);
            // Also find the diff between bounds.center & transform.position
            boundsCenterOffset = bounds.center - transform.position;
        }
        // Every time, update the bounds to the current position
        bounds.center = transform.position + boundsCenterOffset;
        // Check to see whether the bounds are completely offscreen
        Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen);
        if (off != Vector3.zero)
        {
            // If this enemy has gone off the bottom edge of the screen
            if (off.y < 0)
            {
                // then destroy it
                Destroy(this.gameObject);
                Main.enemiesOnScreen--;
            }
        }
    }

    /// <summary>
    /// The enemy is hit by your shoot or ship, action that happens:
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter(Collision coll)
    {
        GameObject other = coll.gameObject;
        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();
                // Enemies don't take damage unless they're onscreen
                // This stops the player from shooting them before they are visible
                bounds.center = transform.position + boundsCenterOffset;
                if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck(bounds,
                BoundsTest.offScreen) != Vector3.zero)
                {
                    Destroy(other);
                    break;
                }
                // Hurt this Enemy

                ShowDamage();

                // Get the damage amount from the Projectile.type & Main.W_DEFS
                health -= Main.W_DEFS[p.type].damageOnHit;
                if (health <= 0)
                {
                    if (alive)
                    {
                        alive = false; // only die once
                        // Tell the Main singleton that this ship has been destroyed
                        Main.S.ShipDestroyed(this);
                        // Destroy this Enemy

                        if (type == 0) { Main.type1killed++; }
                        if (type == 1) { Main.type2killed++; }
                        if (type == 2) { Main.type3killed++; }
                        if (type == 3) { Main.type4killed++; }
                        if (type == 4) { Main.type5killed++; }

                        Destroy(this.gameObject);
                        Debug.Log("Enemy Killed score is " + score);
                        ScoreManager.AddScore(score);
                        if (AudioControl.activeDestroy == 0)
                        {
                            GameObject.FindGameObjectWithTag("Destroy").GetComponent<AudioSource>().Play();
                        }
                        if (AudioControl.activeDestroy == 1)
                        {
                            GameObject.FindGameObjectWithTag("Destroy2").GetComponent<AudioSource>().Play();
                        }
                    }
                }
                Destroy(other);
                break;
        }

    }

    /// <summary>
    /// Blink red
    /// </summary>
    void ShowDamage()
    {
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        remainingDamageFrames = showDamageForFrames;
    }

    /// <summary>
    /// unblink red
    /// </summary>
    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
    }

}
