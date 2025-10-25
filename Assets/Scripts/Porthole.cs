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
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (porthole == 1 && isTracked && GameManager.Instance.lane1s.Count > 0)
        {
            cannonAimer.target = GameManager.Instance.lane1s[0].transform;
        }
        if (porthole == 2 && isTracked && GameManager.Instance.lane2s.Count > 0)
        {
            cannonAimer.target = GameManager.Instance.lane2s[0].transform;
        }
        if (porthole == 3 && isTracked && GameManager.Instance.lane3s.Count > 0)
        {
            cannonAimer.target = GameManager.Instance.lane3s[0].transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Cannon cannon = other.GetComponent<Cannon>();
        cannonAimer = other.GetComponent<CannonAimer>();
        Debug.Log("CannonAimer found: " + (cannonAimer != null));
        // check if the tracked object entered
        if (other.CompareTag("Cannon"))
        {
            Debug.Log("OMG CANNON IN PORTHOLE " + porthole);
            cannonTransform = other.transform;
            cannonTransform.position = this.transform.position;
            //cannonTransform.rotation = this.transform.rotation;
            isTracked = true;
            cannon.canFire = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Cannon cannon = other.GetComponent<Cannon>();
        if (isTracked && other.CompareTag("Cannon"))
        {
            Debug.Log("oh... cannon left porthole " + porthole);
            isTracked = false;
            cannon.canFire = false;
        }
    }
}
