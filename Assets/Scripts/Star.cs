using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public AudioClip success;
    public GameObject starBurst;
    public float volume = 0.7f;

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
        gameManager.ManageVictory();
        
        Instantiate(starBurst, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(success, transform.position, volume);

        Destroy(gameObject);
    }

}
