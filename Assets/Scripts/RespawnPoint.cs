using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RespawnPoint : MonoBehaviour
{
    private Coroutine respawnRoutine; 
    public float waitTimeRespawn;

    public TMP_Text countdownText;
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
        var player = collision.GetComponent<Player>();
        if (collision.CompareTag("Player") && !player.alive && respawnRoutine == null)
        {
            
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
                //Debug.Log("Player exited spawn. Stopping coroutine.");
                StopCoroutine(respawnRoutine);
                countdownText.gameObject.SetActive(false);
                respawnRoutine = null;
            }
        }
    }

    public IEnumerator Respawn(Player player)
    {
        //Debug.Log("Respawning...");
      
        countdownText.gameObject.SetActive(true);
 
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);

        countdownText.gameObject.SetActive(false);

        //yield return new WaitForSeconds(waitTimeRespawn);

        player.RespawnPlayer();
        respawnRoutine = null;
        

    }
}
