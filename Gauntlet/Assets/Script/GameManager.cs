using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Dictates global functionality and manages playerHealth, score, and UI.
 */
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public int playerHealth;
    public int score;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    /// <summary>
    /// Checks to see if GameManager is assigned.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.Log("Game Manager is null");
            }
            return _instance;
        }
    }

    /// <summary>
    /// Ensures that gameManager is persistent between scenes.
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Updates UI to show player's health and score.
    /// </summary>
    private void FixedUpdate()
    {
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + playerHealth;
    }
}
