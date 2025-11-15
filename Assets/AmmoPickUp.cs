using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmmoPickUp : MonoBehaviour
{
    GameObject cannonObject;
    Cannon cannon;
    public int addAmmo;

    private AudioSource audioSource;
    public AudioClip ammoPickUpSound;

    void Start()
    {
        cannonObject = GameObject.FindGameObjectWithTag("Cannon");
        cannon = cannonObject.GetComponent<Cannon>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        var player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player") && player.alive)
        {
            if (cannon.AddAmmo(addAmmo))
            {
                if (ammoPickUpSound != null)
                {
                    AudioSource.PlayClipAtPoint(ammoPickUpSound, transform.position);
                }
                Destroy(gameObject);
            }
        }
    }

}
