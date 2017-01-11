using UnityEngine; // Required for Unity
using System.Collections; // Required for Arrays & other Collections
using System.Collections.Generic; // Required to use Lists or Dictionaries
using UnityEngine.UI;

/// <summary>
/// Runs the shooter game Gameplay
/// </summary>
public class Main : MonoBehaviour
{
    public static int level;
    static public Main S;
    public GameObject[] backgrounds;
    static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;
    public static float bkSize;
    public Text levelat;
    public Text type1;
    public Text type2;
    public Text type3;
    public Text type4;
    public Text type5;
    public Text time;
    public static int type1killed = 0;
    public static int type2killed = 0;
    public static int type3killed = 0;
    public static int type4killed = 0;
    public static int type5killed = 0;
    static public bool[] enemyOn = new bool[5];
    static public int maxEnemies;
    static public int maxScore;
    static public bool[] GEnemyOn = new bool[5];
    static public bool[] SEnemyOn = new bool[5];
    static public bool[] BEnemyOn = new bool[5];
    public static int GTScore;
    public static  int STScore;
    public static int BTScore;
    public static int BTEnemies;
    public static int STEnemies;
    public static int GTEnemies;
    public float pUpPercent;
    float startTime = 0;
    public GameObject[] prefabEnemies;
    public static int enemiesOnScreen = 0;
    public float enemySpawnPerSecond = 0.5f; // # Enemies/second
    public float enemySpawnPadding = 1.5f; // Padding for position
    public WeaponDefinition[] weaponDefinitions;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] {
    WeaponType.blaster, WeaponType.blaster,
    WeaponType.spread,
    WeaponType.shield };
    public static int bkg = 0;
    public bool ________________;
    public WeaponType[] activeWeaponTypes;
    public float enemySpawnRate; // Delay between Enemy spawns

    /// <summary>
    /// Set up the game
    /// </summary>
    void Awake()
    {
        S = this;
        // Set Utils.camBounds
        Utils.SetCameraBounds(this.GetComponent<Camera>());
       
        enemySpawnRate = 1f / enemySpawnPerSecond; // 1

        // A generic Dictionary with WeaponType as the key
        W_DEFS = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            W_DEFS[def.type] = def;
        }
    }

    /// <summary>
    /// Set up the weapons
    /// </summary>
    /// <param name="wt"></param>
    /// <returns></returns>
    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        // Check to make sure that the key exists in the Dictionary
        // Attempting to retrieve a key that didn't exist, would throw an error,
        // so the following if statement is important.
        if (W_DEFS.ContainsKey(wt))
        {
            return (W_DEFS[wt]);
        }
        // This will return a definition for WeaponType.none,
        // which means it has failed to find the WeaponDefinition
        return (new WeaponDefinition());
    }
    
    /// <summary>
    /// load text and background
    /// </summary>
    void Start()
    {
        levelat.text = "Level: " + Main.level;
        enemiesOnScreen = 0;
        startTime = Time.time;

        if (bkg > 0) { 
        GameObject bkgd = Instantiate(backgrounds[bkg-1]);//make cool background
        
            //Set backgroung size
            Vector3 bkgSize = new Vector3(bkSize, bkSize,1f);
            bkgd.transform.localScale = bkgSize;
        }

        activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
        for (int i = 0; i < weaponDefinitions.Length; i++)
        {
            activeWeaponTypes[i] = weaponDefinitions[i].type;
        }

        switch (level) //set up the game based on level player is on
        {
            case 1: //bronze
                enemyOn = BEnemyOn;
                maxEnemies = BTEnemies;
                maxScore = BTScore;
                break;

            case 2: //silver
                enemyOn = SEnemyOn;
                maxEnemies = STEnemies;
                maxScore = STScore;
                break;

            case 3: //gold
                enemyOn = GEnemyOn;
                maxEnemies = GTEnemies;
                maxScore = GTScore;
                break;
        }
        // Invoke call SpawnEnemy() once after a 2 second delay
        Invoke("SpawnEnemy", enemySpawnRate); // 2 
    }

    /// <summary>
    /// Wait a second before enemies respawn to give player a sec
    /// </summary>
    /// <param name="delay"></param>
    public void DelayedRestart(float delay)
    {
        // Invoke the Restart() method in delay seconds
        Invoke("Restart", delay);
    }

    /// <summary>
    /// Restart the game
    /// </summary>
    public void Restart()
    {
        // Reload _Scene_0 to restart the game
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Make enemy to fight
    /// </summary>
    public void SpawnEnemy()
    {
        Debug.Log("Enemies On Screen : " + enemiesOnScreen);

        if (enemiesOnScreen < maxEnemies)
        {
            enemiesOnScreen++;
            // Pick a random Enemy prefab to instantiate
            int ndx = Random.Range(0, prefabEnemies.Length);
            while (enemyOn[ndx] == false) { ndx++; ndx = ndx % prefabEnemies.Length; }

            GameObject go = Instantiate(prefabEnemies[ndx]) as GameObject;

            // Position the Enemy above the screen with a random x position
            Vector3 pos = Vector3.zero;
            float xMin = Camera.main.ViewportToWorldPoint(Vector3.zero).x + enemySpawnPadding;
            float xMax = Camera.main.ViewportToWorldPoint(Vector3.one).x - enemySpawnPadding;
            pos.x = Random.Range(xMin, xMax);
            pos.y = Camera.main.ViewportToWorldPoint(Vector3.one).y + enemySpawnPadding;
            go.transform.position = pos;
        }
            // Call SpawnEnemy() again in a couple of seconds
            Invoke("SpawnEnemy", enemySpawnRate); // 3
        
    }
    /// <summary>
    /// ship is destroyed
    /// </summary>
    /// <param name="e"></param>
    public void ShipDestroyed(Enemy e)
    {
        enemiesOnScreen--;
        // Potentially generate a PowerUp
        if (Random.value <= e.powerUpDropChance)
        {
            // Random.value generates a value between 0 & 1 (though never == 1)
            // If the e.powerUpDropChance is 0.50f, a PowerUp will be generated
            // 50% of the time. For testing, it's now set to 1f.
            // Choose which PowerUp to pick
            // Pick one from the possibilities in powerUpFrequency
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];
            // Spawn a PowerUp

            if (Random.value >1-pUpPercent)
            {
                GameObject go = Instantiate(prefabPowerUp) as GameObject;
                PowerUp pu = go.GetComponent<PowerUp>();
                // Set it to the proper WeaponType
                pu.SetType(puType);
                // Set it to the position of the destroyed ship
                pu.transform.position = e.transform.position;
            }
        }
    }
    /// <summary>
    /// update time and text of enemies killed
    /// </summary>
    void Update()
    {

        type1.text = "E1K: " + type1killed;
        type2.text = "E2K: " + type2killed;
        type3.text = "E3K: " + type3killed;
        type4.text = "E4K: " + type4killed;
        type5.text = "E5K: " + type5killed;

        time.text = ("Time: " + (Time.time-startTime) + "secs");

    }
}