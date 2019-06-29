using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int numberOfStars;

    private void Update()
    {
        ManageVictory();
    }

    private void ManageVictory()
    {
        if(numberOfStars <= 0)
        {
            print("Win");
        }
    }
}
