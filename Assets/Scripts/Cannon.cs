using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public Transform firepoint;
    public float fixedY = 0.68f;
    public float launchVelocity = 700f;
    private Vector3 offset = new Vector3(0, 2f, 0);
    public bool canFire = false;
    public int currentAmmo;
    public int maxAmmo;

    public GameObject explosionPrefab;
    public GameObject firePoint;

    public AudioSource audioSource;
    public AudioClip shootCannon;

    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        //lockedRotX = transform.rotation.eulerAngles.x;
        //lockedRotZ = transform.rotation.eulerAngles.z;
        uiManager = GameObject.Find("Canvas (1)").GetComponent<UIManager>();
        UpdateAmmo();
    }
    void LateUpdate()
    {
        Vector3 p = transform.position;
        p.y = fixedY;
        transform.position = p;

        //WE NEED ROTATION NOW
        //Vector3 e = transform.rotation.eulerAngles;
        //e.x = lockedRotX;
        //e.z = lockedRotZ;
        //transform.rotation = Quaternion.Euler(e);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canFire && currentAmmo!=0)
        {
            Instantiate(explosionPrefab, firePoint.transform.position, firePoint.transform.rotation);
            AudioSource.PlayClipAtPoint(shootCannon, transform.position);

            GameObject cannonBall = Instantiate(projectile, firepoint.position, firepoint.rotation);

            Rigidbody rb = cannonBall.GetComponent<Rigidbody>();

            // Directly set initial velocity instead of AddRelativeForce
            rb.velocity = firepoint.TransformDirection(Vector3.right * launchVelocity);
            currentAmmo--;
            UpdateAmmo();
        }
    }

    public bool AddAmmo(int pickUpAmmo)
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo+=pickUpAmmo;
            if (currentAmmo >= maxAmmo){currentAmmo = maxAmmo;}
            UpdateAmmo();
            return true;
            
        }
            return false;
    }

    public int GetCurrentAmmo(){return currentAmmo;}
    public int GetMaxAmmo(){return maxAmmo; }

    public void UpdateAmmo(){uiManager.UpdateAmmoUI(currentAmmo, maxAmmo);}

}
