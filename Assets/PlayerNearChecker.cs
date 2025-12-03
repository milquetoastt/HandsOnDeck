using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearChecker : MonoBehaviour
{
    bool playerNear = false;
    Cannon cannon;

     void Start()
     {
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
     }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player.alive == true && cannon.GetCurrentAmmo() > 0)
            {
                playerNear = true;
                updatePlayerIcon(player.name, true);  
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player.alive == true )
            {
                playerNear = false;
                updatePlayerIcon(player.name, false);
            }
        }
    }

    public void updatePlayerIcon(string name,bool near)
    {

        //give canvas the player that is in the button and wether they are ner or not
    }
}
