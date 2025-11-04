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
    public Cannon cannon;

    public bool cannonReady;
    public bool playerReady;
    void Start()
    {
        cannonAimer = cannon.GetComponent<CannonAimer>();
    }

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

            if (playerReady && cannonReady)
        {
            cannon.canFire = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Cannon cannon = other.GetComponent<Cannon>();
        
        Debug.Log("CannonAimer found: " + (cannonAimer != null));
        // check if the tracked object entered
        if (other.CompareTag("Cannon"))
        {
            Debug.Log("OMG CANNON IN PORTHOLE " + porthole);
            cannonTransform = other.transform;
            cannonTransform.position = this.transform.position;
            //cannonTransform.rotation = this.transform.rotation;
            isTracked = true;
            cannonReady = true;
        }

        if (other.CompareTag("Player"))//one player must be next to cannon to shoot
        {
            Debug.Log("OMG PLAYER IN PORTHOLE " + porthole);
            playerReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Cannon cannon = other.GetComponent<Cannon>();
        if (isTracked && other.CompareTag("Cannon"))
        {
            Debug.Log("oh... cannon left porthole " + porthole);
            isTracked = false;
            cannon.canFire = false;
            cannonReady = false;
        }
        if (other.CompareTag("Player"))//one player must be next to cannon to shoot
        {
            Debug.Log("oh... player left porthole  " + porthole);
            cannon.canFire = false;
            playerReady = false;
        }
    }
}
