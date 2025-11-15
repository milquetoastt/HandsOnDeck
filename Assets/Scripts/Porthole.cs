using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porthole : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform cannonTransform;
    private bool isTracked = false;
    public int porthole;
    public CannonAimer cannonAimer;
    public Cannon realCannon;

    public bool cannonReady;
    public bool playerReady;

    void Start()
    {
        playerReady = false;
        cannonReady = false;

        cannonAimer = realCannon.GetComponent<CannonAimer>();
        cannonAimer.numLane = 0;
    }
    //every porthole is checking for their own colliders

    // Update is called once per frame
    void Update()//if in porthole show crosshair
    {
        if (porthole == 2 && isTracked)
        {
            cannonAimer.numLane = 2;

            /*if (GameManager.Instance.lane2s[0] != null)
            {
                cannonAimer.target = GameManager.Instance.lane2s[0].transform;
                cannonAimer.numLane = 2;
            }*/
        }

        if (isTracked)
        {
            CheckIfCanFire(realCannon);
        }

    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannon"))
        {
            Cannon cannon = other.GetComponent<Cannon>();
            cannonTransform = other.transform;
            cannonTransform.position = this.transform.position;
            isTracked = true;
        }

        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player.alive == true)
            {
                playerReady = true;
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        Cannon cannon = other.GetComponent<Cannon>();
        Player player = other.GetComponent<Player>();
        if (isTracked && other.CompareTag("Cannon"))
        {
            isTracked = false;
        }
        if (other.CompareTag("Player"))
        {
            playerReady = false;
        }

        cannonAimer.numLane = 0;
    }

    public void CheckIfCanFire(Cannon cannon)
    {
        if (playerReady && isTracked)
        {
            cannon.canFire = true;
        }
        else                                       
        {
            cannon.canFire = false;
        }
    }
}
