using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public Transform firepoint;
    public float launchVelocity = 700f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if player collider with you is dead, if then, can't hold cannon, if player colliding with you is alive then can shoot
        //press button to shoot for now, shoot a ball at a certain velocity
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject cannonBall = Instantiate(projectile, firepoint.position, firepoint.rotation);
            cannonBall.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(launchVelocity, 0, 0));
        }
    }
}
