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
    }
    //every porthole is checking for their own colliders

    // Update is called once per frame
    void Update()
    {
        if (porthole == 1 && isTracked && GameManager.Instance.lane1s.Count > 0)
        {
            if (GameManager.Instance.lane1s[0] != null)
            {
                cannonAimer.target = GameManager.Instance.lane1s[0].transform;
            }
        }

        if (porthole == 2 && isTracked && GameManager.Instance.lane2s.Count > 0)
        {
            if (GameManager.Instance.lane2s[0] != null)
            {
                cannonAimer.target = GameManager.Instance.lane2s[0].transform;
            }
        }

        if (porthole == 3 && isTracked && GameManager.Instance.lane3s.Count > 0)
        {
            if (GameManager.Instance.lane3s[0] != null)
            {
                cannonAimer.target = GameManager.Instance.lane3s[0].transform;
            }
        }
        if (isTracked)
        {
            CheckIfCanFire(realCannon);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("CannonAimer found: " + (cannonAimer != null));
        // check if the tracked object entered
        if (other.CompareTag("Cannon"))
        {
            Cannon cannon = other.GetComponent<Cannon>();

            Debug.Log("OMG CANNON IN PORTHOLE " + porthole);
            cannonTransform = other.transform;
            cannonTransform.position = this.transform.position;
            //cannonTransform.rotation = this.transform.rotation;
            isTracked = true;
        }

        if (other.CompareTag("Player"))//one player must be next to cannon to shoot
        {
            Debug.Log("OMG PLAYER IN PORTHOLE " + porthole);

            Player player = other.GetComponent<Player>();

            if (player.alive == true)
            {
                playerReady = true;
                Debug.Log("player is alive can fire");
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        Cannon cannon = other.GetComponent<Cannon>();
        Player player = other.GetComponent<Player>();
        if (isTracked && other.CompareTag("Cannon"))
        {
            Debug.Log("oh... cannon left porthole " + porthole);
            isTracked = false;
        }
        if (other.CompareTag("Player"))//one player must be next to cannon to shoot
        {
            Debug.Log("oh... player left porthole  " + porthole);
            playerReady = false;
        }

       
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

        Debug.Log(cannon.canFire);
    }
}
