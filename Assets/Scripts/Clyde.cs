using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde : MonoBehaviour
{
    // Rigidbody
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody
        rb = GetComponent<Rigidbody2D>();

        // Shooting Elements
        clydeShootTime = clydeShootTimeStart;
    }

    // Update is called once per frame
    void Update()
    {
        // Keeps Clyde in screen
        KeepClydeInScreen();

        // Checks how long Clyde has to wait before shooting again
        ClydeShootYet();
        
        // Checks If Clyde hits the side of the screen
        BounceClyde();
    }

    void FixedUpdate()
    {
        // Movement
        MoveClyde();

        // Shooting
        ClydeShoot();
    }

    // Movement Elements
    public float speed;

    // Used to manipulate Clyde's X vector
    public int moveDir;

    // Change Clyde's X vector when he hits the screen
    void BounceClyde()
    {
        if (transform.position.x <= GameManager.bottomLeft.x - transform.position.x / 7)
        {
            moveDir = 1;
        }

        if (transform.position.x >= GameManager.topRight.x - transform.position.x / 7)
        {
            moveDir = -1;
        }
    }

    // Moves Clyde
    void MoveClyde()
    {
        rb.velocity = new Vector2 (moveDir * speed, -speed / 6) * Time.deltaTime;
    }

    // Keeps Clyde within the screen
    void KeepClydeInScreen()
    {
        rb.transform.position = new Vector2(Mathf.Clamp(transform.position.x, GameManager.bottomLeft.x - transform.position.x / 8, GameManager.topRight.x - transform.position.x / 8), Mathf.Clamp(transform.position.y, GameManager.bottomLeft.y * 1.25f, GameManager.topRight.y * 1.25f));

        // Destroy clyde if he goes below the screen
        if (transform.position.y < GameManager.bottomLeft.y - 1)
        {
            Destroy(gameObject);
        }
    }

    // Shooting Elements
    public GameObject clydeLaser;
    
    public float clydeShotSpeed;
    public float clydeShootTimeStart;
    private float clydeShootTime;
    private bool clydeShootBool = false;

    // Checks how long Clyde has to wait before shooting again
    void ClydeShootYet()
    {
        if (clydeShootTime <= 0)
        {
            clydeShootBool = true;
        }

        clydeShootTime -= Time.deltaTime;
    }

    // Clyde Shoots
    void ClydeShoot()
    {
        if (clydeShootBool)
        {
            // Resets Clyde's shoot boolean
            clydeShootBool = false;

            // Resets Cylde's Shoot timer
            clydeShootTime = clydeShootTimeStart;

            // Physically creates Clyde's bullet
            GameObject clydeShot = Instantiate(clydeLaser, new Vector2 (transform.position.x, transform.position.y), Quaternion.identity);

            // Physically moves Clyde's bullet
            clydeShot.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, -clydeShotSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.PlayerDamaged();

            Debug.Log("Player touched ghost");
            Debug.Log("Player Health: " + GameManager.health);
        }
    }
}
