using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
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
        Debug.Log("collided");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collide with Respawn");
            var player = collision.GetComponent<Player>();
            StartCoroutine(player.RespawnPlayer());
        }
    }
}
