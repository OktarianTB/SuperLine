using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    new Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (!rigidbody)
        {
            Debug.LogWarning("Rigidbody component is missing from the player");
        }
    }

    void Update()
    {
        CheckGameStart();
    }

    private void CheckGameStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
