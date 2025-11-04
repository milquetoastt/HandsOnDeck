using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    public GameObject deathBox;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
            }

            CleanUpBall();
        }

        if (collision.CompareTag("Ground"))
        {
            var deck = collision.GetComponent<PlayerShip>();
            if (deck != null)
            {
                deck.DamageShip();
            }

            Instantiate(deathBox, transform.position, Quaternion.identity);

            CleanUpBall();
        }

    }

    private IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void CleanUpBall()
    {
        StopAllCoroutines();
        DestroyBall();
    }
}
