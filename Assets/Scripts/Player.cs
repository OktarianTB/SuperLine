using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    new Rigidbody2D rigidbody;
    public bool gameHasStarted = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();

        if (!rigidbody)
        {
            Debug.LogWarning("Rigidbody component is missing from the player");
        }
        
        if (!gameManager)
        {
            Debug.LogWarning("Player can't find the Game Manager");
        }
    }

    void Update()
    {
        if (!gameHasStarted)
        {
            CheckGameStart();
        }
    }

    private void CheckGameStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            gameHasStarted = true;
        }
    }

    private void OnBecameInvisible()
    {
        if (gameManager)
        {
            gameManager.PlayerDeath();
        }
    }


}
