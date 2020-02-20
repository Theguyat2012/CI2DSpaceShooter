using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{
    public AudioClip enemyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ExitedScreen();
    }

    // Destroy self if left screen to save memory
    void ExitedScreen()
    {
        if (transform.position.y > GameManager.topRight.y)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //Plays clip at the camera's position for maximum volume. Does not get interrupted when the script's object is destroyed.
            AudioSource.PlayClipAtPoint(enemyExplosion, Camera.main.transform.position, 10f);

            // Destroy Enemy Target
            Destroy(col.gameObject);

            // Destroy self also, so you can't get a collateral
            Destroy(gameObject);

            // Scoring Elements
            GameManager.PlayerScored();
        }
    }
}
