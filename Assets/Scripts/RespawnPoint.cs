using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private Coroutine respawnRoutine; 
    public float waitTimeRespawn;
    // Start is called before the first frame update
    void Start()
    {
        respawnRoutine = null;
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
            respawnRoutine = StartCoroutine(Respawn(player));
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (respawnRoutine != null)
            {
                Debug.Log("Player exited spawn. Stopping coroutine.");
                StopCoroutine(respawnRoutine);
                respawnRoutine = null;
            }
        }
    }

    public IEnumerator Respawn(Player player)
    {
        Debug.Log("Respawning...");
        yield return new WaitForSeconds(waitTimeRespawn);

        player.RespawnPlayer();
        respawnRoutine = null;

    }
}
