﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered hazard.");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.Respawn();
        }
        else
        {
            Debug.Log("Something entered the hazard.");
        }

    }
}
