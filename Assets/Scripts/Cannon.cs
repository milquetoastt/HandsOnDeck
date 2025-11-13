using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public Transform firepoint;
    public float launchVelocity = 700f;
    private Vector3 offset = new Vector3(0, 2f, 0);
    public bool canFire = false;
    public int currentAmmo;
    public int maxAmmo;

    public float fixedY = 0f;     // The Y height you want locked
    private float lockedRotX = 0f;
    private float lockedRotY = 0f;
    private float lockedRotZ = 0f;

    public AudioSource audioSource;
    public AudioClip shootCannon;
    // Start is called before the first frame update
    void Start()
    {
        lockedRotX = transform.rotation.eulerAngles.x;
        lockedRotY = transform.rotation.eulerAngles.y;
        lockedRotZ = transform.rotation.eulerAngles.z;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canFire && currentAmmo!=0)
        {
            AudioSource.PlayClipAtPoint(shootCannon, transform.position);

            GameObject cannonBall = Instantiate(projectile, firepoint.position, firepoint.rotation);

            Rigidbody rb = cannonBall.GetComponent<Rigidbody>();

            // Directly set initial velocity instead of AddRelativeForce
            rb.velocity = firepoint.TransformDirection(Vector3.right * launchVelocity);
            currentAmmo--;
        }
    }
    void LateUpdate()
    {
        // --- 1. Freeze Y position ---
        Vector3 p = transform.position;
        p.y = fixedY;
        transform.position = p;

        // --- 2. Freeze X and Z rotation ---
        Vector3 e = transform.rotation.eulerAngles;
        e.x = lockedRotX;
        e.y = lockedRotY;
        e.z = lockedRotZ;
        transform.rotation = Quaternion.Euler(e);
    }
    public bool AddAmmo(int pickUpAmmo)
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo+=pickUpAmmo;
            if (currentAmmo >= maxAmmo){currentAmmo = maxAmmo;}
            return true;
        }
            return false;
    }

    public int GetCurrentAmmo(){return currentAmmo;}
    public int GetMaxAmmo(){return maxAmmo; }

}
