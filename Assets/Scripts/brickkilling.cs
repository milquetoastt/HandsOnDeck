using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickkilling : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Brick hit something", collision);
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            //Debug.Log("player ran into death spot");
            player.Die();
        }
    }
}
