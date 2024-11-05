using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // INHERITANCE (inherits from MonoBehaviour)
{
    private GameManager gameManager;
    public float speed; // ENCAPSULATION (public property for speed)

    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed); 

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(5); // ABSTRACTION (update score without exposing details)
        }
    }
}
