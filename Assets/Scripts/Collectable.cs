using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider2d;
    private static int coinCount;
    private void Start()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinCount++;
            Debug.Log("Coint count: " + coinCount);
            spriteRenderer.enabled = false;
            boxCollider2d.enabled = false;
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length + 10);
        }
    }
}
