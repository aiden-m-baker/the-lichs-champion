using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private PlayerEntity[] playerEntities;
    [SerializeField]
    private SpriteRenderer[] spriteRenderers;

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[4];
        playerEntities = new PlayerEntity[4];
        spriteRenderers = new SpriteRenderer[4];
    }

    // Update is called once per frame
    void Update()
    {
        // update list of players, if the only if the amount of players have changed, or
        // if the other lists are not the same size as the player list
        if (players.Count() != GameObject.FindGameObjectsWithTag("Player").Length)
        {
            // update all arrays
            UpdateFields();
            // check if all players have their colors set
            //SetPlayerColors();
        }
        
        // if any of the players are white
        if (WhitePlayerChecker())
        {
            // set the player colors
            SetPlayerColors();
        }
    }
    private void UpdateFields()
    {
        // reassign all players to the new array (explicitly cast to list)
        players = GameObject.FindGameObjectsWithTag("Player");

        // reassign all player entities to the new list
        for (int i = 0; i < players.Count(); i++)
        {
            playerEntities[i] = players[i].GetComponent<PlayerEntity>();
            spriteRenderers[i] = players[i].GetComponent<SpriteRenderer>();
        }
    }
    /// <summary>
    /// Checks whether all the players have their colors set
    /// </summary>
    /// <returns>Returns a boolean, false if their colors are not set, true if they are</returns>
    private bool WhitePlayerChecker()
    {
        // check every player
        for (int i = 0; i < players.Count(); i++)
        {
            // if any of the player's colors are white, return true
            if (spriteRenderers[i].color == Color.white)
            {
                return true;
            }
        }
        // if all players have their colors set, return false
        return false;
    }
    private void SetPlayerColors()
    {
        // player 1 red
        if (spriteRenderers[0])
            spriteRenderers[0].color = Color.red;
        // player 2 red
        if (spriteRenderers[1])
            spriteRenderers[1].color = Color.blue;
        // player 3 red
        if (spriteRenderers[2])
            spriteRenderers[2].color = Color.yellow;
        // player 4 red
        if (spriteRenderers[3])
            spriteRenderers[3].color = Color.green;
    }
}
