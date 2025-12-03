using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearChecker : MonoBehaviour
{
    Cannon cannon;
    private UIManager uiManager;

     void Start()
     {
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
        uiManager = GameObject.Find("Canvas (1)").GetComponent<UIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player.alive == true && cannon.GetCurrentAmmo() > 0)
            {
                
                updatePlayerIcon(player.name, true);
            }
            else if (player.alive == false)
            {
                updatePlayerIcon(player.name, false);

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player.alive == true && cannon.GetCurrentAmmo() > 0)
            {
                
                updatePlayerIcon(player.name, true);
            }
            else if (player.alive == false)
            {
                updatePlayerIcon(player.name, false);

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
                
                updatePlayerIcon(player.name, false);
            }
        }
    }

    public void updatePlayerIcon(string name,bool near)
    {
        uiManager.playerIconStatus(name, near, 1);
    }
}
