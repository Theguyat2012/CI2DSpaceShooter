using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ExitedScreen();
    }

    void ExitedScreen()
    {
        if (transform.position.y <= GameManager.bottomLeft.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.PlayerDamaged(); // Deals damage to player

            Destroy(gameObject); // Destroys bullet after hitting the player

            Debug.Log("Player got shot");
            Debug.Log("Player Health: " + GameManager.health);
        }
    }
}
