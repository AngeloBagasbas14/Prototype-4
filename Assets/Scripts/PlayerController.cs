using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour // INHERITANCE (inherits from MonoBehaviour)
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerStrength = 15.0f;
    public float speed = 5.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;

    // Reference to the GameManager
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        // Find and reference the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name + " with tag: " + other.tag); // Log all trigger interactions

        if (other.CompareTag("MultiplierIcon"))
        {
            Debug.Log("Player triggered MultiplierIcon: " + other.gameObject.name);
            if (gameManager != null)
            {
                gameManager.GameOver(); // ABSTRACTION (GameOver method abstracts game-ending behavior)
             
                Debug.Log("Game Over triggered");
            }
            else
            {
                Debug.LogError("GameManager reference is null");
            }
        }

        // Existing logic for powerups
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine()); // ABSTRACTION (coroutine abstracts countdown behavior)
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine() // ABSTRACTION (coroutine abstracts power-up timing logic)
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerStrength, ForceMode.Impulse);
            Debug.Log("Player Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
