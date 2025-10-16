using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonBall : MonoBehaviour
{
    public GameObject deathBox;
    // Start is called before the first frame update
    void Start()
    {
        
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
            player.Die();
        }

        if (collision.CompareTag("Ground"))
        {
            
            Vector3 coords = transform.position;
            Debug.Log("enemy cannonball hit ground at " + coords);
            Instantiate(deathBox, coords, Quaternion.identity);
        }

    }
}
