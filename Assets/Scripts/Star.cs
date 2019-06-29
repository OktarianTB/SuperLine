﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager)
        {
            gameManager.numberOfStars++;
        }
        else
        {
            Debug.LogWarning("Star can't find the Game Manager");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.numberOfStars--;
        Destroy(gameObject);
    }

}