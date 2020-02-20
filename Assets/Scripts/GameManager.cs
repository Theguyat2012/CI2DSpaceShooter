using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // This project was influenced by the Unity Space Shooter Tutorial

    // Screen Elements
    public Camera mainCam;
    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    // Enemy Movement Elements
    private int randMoveDir; // Used to randomly generate start movement direction

    // Start is called before the first frame update
    void Start()
    {
        // Access the bottom left corner of camera and top right of camera
        bottomLeft = mainCam.ScreenToWorldPoint(new Vector2(0f, 0f));
        topRight = mainCam.ScreenToWorldPoint(new Vector2 (mainCam.pixelWidth, mainCam.pixelHeight));
    }

    // Update is called once per frame
    void Update()
    {
        // Controls when the enemy spawns
        EnemySpawnTimer();

        // Physically spawns enemies
        SpawnClyde();

        // UI updates
        ScoreUpdate();
        HealthUpdate();
    }

    // Enemies
    public Clyde clyde;

    // Enemy Spawn Time Elements
    public float enemySpawnTimeStart;
    private float enemySpawnTime;
    private bool canSpawnEnemy;

    // Enemy Functions

    // Keeps enemy spawn timing in check
    private void EnemySpawnTimer()
    {
        if (enemySpawnTime <= 0)
        {
            canSpawnEnemy = true;
        }
        else
        {
            canSpawnEnemy = false;
            enemySpawnTime -= Time.deltaTime;
        }
    }

    private Clyde clydeObject;

    // Spawns Clydes
    private void SpawnClyde()
    {

        if (canSpawnEnemy)
        {
            RandomDirection();

            // Resets timer after spawning a Clyde
            enemySpawnTime = enemySpawnTimeStart;

            // Physically spawns a Clyde
            clydeObject = Instantiate(clyde, new Vector2(Random.Range(bottomLeft.x + (bottomLeft.x / 8), topRight.x - (topRight.x / 8)), topRight.y * 1.2f), Quaternion.identity);

            // Gived each indiidual Clyde his own moveDir
            if (randMoveDir <= 0.5f)
            {
                clydeObject.moveDir = -1;
            }
            else
            {
                clydeObject.moveDir = 1;
            }
        }
    }
    
    private void RandomDirection()
    {
        // Get -1 or 1 at random, used for random x vector move direction
        randMoveDir = Random.Range(0, 2); // Picks 0 or 1 randomly

        Debug.Log(randMoveDir);
    }

    // Player Elements

    // Health Elements
    public static int health = 10;

    // Score Elements
    public static int score;

    public static void PlayerScored()
    {
        score += 50;

        Debug.Log(score);
    }

    // Player Health

    // Used Damage Player
    public static void PlayerDamaged()
    {
        health -= 1;
    }

    // UI Elements
    public Text scoreText;
    public Text healthText;

    private void ScoreUpdate()
    {
        scoreText.text = score.ToString();
    }

    private void HealthUpdate()
    {
        healthText.text = health.ToString();
    }
}
