using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Rigidbody
    private Rigidbody2D rb;

    // SFX Sound Effects
    public AudioSource playerLaserShot;

    // Paue Elements
    public GameObject pauseMenu;
    public GameObject playHud;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody
        rb = GetComponent<Rigidbody2D>();

        // Shooting
        shootTime = shootTimeStart;
    }

    // Movement Elements
    public float speed;
    private float moveHorizontal;
    private float moveVertical;

    // Update is called once per frame
    void Update()
    {
        // Movement
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        // Shooting
        if (Input.GetKey(KeyCode.Space)) // Checks if player presses the shoot button
        {
            pressedShoot = true;
        }
        else
        {
            pressedShoot = false;
        }

        // Pause
        if (Input.GetKeyDown (KeyCode.Escape) && pauseMenu.activeInHierarchy == false)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            playHud.SetActive(false);
        }
        else if  (Input.GetKeyDown (KeyCode.Escape) && pauseMenu.activeInHierarchy == true)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            playHud.SetActive(true);
        }
        
        shootTime -= Time.deltaTime;

        KeepPlayerInScreen();
    }

    void FixedUpdate()
    {
        // Movement
        rb.velocity = new Vector2 (moveHorizontal * speed, moveVertical * speed) * Time.deltaTime;

        // Shooting
        if (pressedShoot)
        {
            Shoot();
        }
    }

    // Shooting Elements
    public GameObject laser;
    public float shotSpeed;
    public float shootTimeStart;
    private float shootTime;
    private bool pressedShoot;

    void Shoot()
    {
        if (shootTime <= 0)
        {
            GameObject shot;

            playerLaserShot.Play();

            shot = Instantiate(laser, transform.position, Quaternion.identity);

            shot.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, shotSpeed * Time.deltaTime);

            shootTime = shootTimeStart;

            if (shot.transform.position.y > GameManager.topRight.y)
            {
                Destroy (shot);
            }
        }
    }

    // Keep Player with screen
    void KeepPlayerInScreen()
    {
        transform.position = new Vector2 (Mathf.Clamp(transform.position.x, GameManager.bottomLeft.x - transform.position.x / 7, GameManager.topRight.x - transform.position.x / 7), Mathf.Clamp(transform.position.y, GameManager.bottomLeft.y - transform.position.y / 7, GameManager.topRight.y - transform.position.y / 7));
    }
}
