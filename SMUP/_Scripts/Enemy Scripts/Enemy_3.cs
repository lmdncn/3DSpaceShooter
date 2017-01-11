using UnityEngine;
using System.Collections;
// Enemy_3 extends Enemy

/// <summary>
/// Special type of enemy : extends Enemy
/// </summary>
public class Enemy_3 : Enemy
{
    // Enemy_3 will move following a Bezier curve in the top of screen and shoot at hero
    // interpolation between more than two points.
    public Vector3[] points;
    public int score3;
    public float birthTime;
    public float lifeTime = 10;
    public Weapon gun ;

    private int fireCounter = 0;

    // Again, Start works well because it is not used by Enemy
    void Start()
    {
        fireCounter = 0;
        type = 3;
        setScore(Enemies.getScoresave(type));
        points = new Vector3[3]; // Initialize points

        // The start position has already been set by Main.SpawnEnemy()

        points[0] = pos;
        // Set xMin and xMax the same way that Main.SpawnEnemy() does
        float xMin = Camera.main.ViewportToWorldPoint(Vector3.zero).x + Main.S.enemySpawnPadding;
        float xMax = Camera.main.ViewportToWorldPoint(Vector3.one).x - Main.S.enemySpawnPadding;
        Vector3 v;
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = Random.Range(Camera.main.ViewportToWorldPoint(Vector3.one).y, Camera.main.ViewportToWorldPoint(Vector3.zero).y);
        v.y = v.y - 60;
        points[1] = v;
        // Pick a random final position above the top of the screen
        v = Vector3.zero;
        v.y = pos.y;
        v.x = Random.Range(xMin, xMax);
        points[2] = v;
        // Set the birthTime to the current time
        birthTime = Time.time;
    }

    /// <summary>
    /// Movement of enemy overide
    /// </summary>
    public override void Move()
    {
        fireCounter++;
        fireCounter = fireCounter % 120;
        if (fireCounter == 0 && (Time.time - birthTime)>3)
        {
            gun.EnemyFire();
        }

        // Bezier curves work based on a u value between 0 & 1
        float u = (Time.time - birthTime) / lifeTime;
        if (u > 1)
        {
            // This Enemy_3 has finished its life
            Destroy(this.gameObject);
            Main.enemiesOnScreen--;
            return;
        }
        // Interpolate the three Bezier curve points
        Vector3 p01, p12;
        u = u - 0.2f * Mathf.Sin(u * Mathf.PI * 2);
        p01 = (1 - u) * points[0] + u * points[1];
        p12 = (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * p01 + u * p12;
    }

  
}