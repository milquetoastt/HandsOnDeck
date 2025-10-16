using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{
    // Start is called before the first frame update
    public int fixTimer = 10;
    void Start()
    {
        StartCoroutine(FixInSeconds());
    }

    private IEnumerator FixInSeconds()
    {
        yield return new WaitForSeconds(fixTimer);
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Death box hit something");
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("player ran into death spot");
            player.Die();
        }
    }
}
