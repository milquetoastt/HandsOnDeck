using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    GameObject cannonObject;
    Cannon cannon;
    // Start is called before the first frame update
    void Start()
    {
        cannonObject = GameObject.FindGameObjectWithTag("Cannon");
        cannon = cannonObject.GetComponent<Cannon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
