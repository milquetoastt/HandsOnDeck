using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public bool alive;
    private SpriteRenderer playerSprite;

    public float waitTime; 
    void Start()
    {
        alive = true; 
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if fallen off hole/get hit by enemy
        if (!alive)
        {
            Die();
        }
    }
    
    public IEnumerator RespawnPlayer() //StartCoroutine(RespawnPlayer());
    {
        Debug.Log("Respawning");
        yield return new WaitForSeconds(waitTime);
        playerSprite.color = new Color(1, 0, 0, 1);
        alive = true;
    }

    public void Die()
    {
        Debug.Log("YouDied");
        //also disable button press? Probably will just have to check if other player near cannon
        playerSprite.color = new Color(1,0,0,0.3f);//make player a ghost? 

    }
}
