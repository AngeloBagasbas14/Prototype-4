using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private GameManager gameManager; // // ENCAPSULATION (if GameManager has private variables with public getters/setters)
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Add a listener to call SetDifficulty when the button is clicked
        button.onClick.AddListener(SetDifficulty); // ABSTRACTION (using Unity’s higher-level button click handling)
    }

    void SetDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked");
        gameManager.StartGame();
        gameObject.SetActive(false); // Hide the start button after clicking
    }
}
