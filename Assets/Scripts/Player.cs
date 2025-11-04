using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public bool alive;
    private SpriteRenderer playerSprite;

    [SerializeField] private SkinnedMeshRenderer faceRenderer;
    [SerializeField] private SkinnedMeshRenderer haloRenderer;

    private Material materialInstance;

    void Start()
    {
        alive = alive;
        playerSprite = GetComponent<SpriteRenderer>();
        materialInstance = faceRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeExpressionOffset(0.00f);
        haloRenderer.enabled = false;
        //if fallen off hole/get hit by enemy
        if (!alive)
        {
            ChangeExpressionOffset(0.25f);
            haloRenderer.enabled = true;
            Die();
        }
    }
    
    public void RespawnPlayer() //StartCoroutine(RespawnPlayer());
    {
        ChangeExpressionOffset(0f);
        Debug.Log("Player Respawn");
        playerSprite.color = new Color(1, 0, 0, 1);
        alive = true;
    }

    public void Die()
    {
        Debug.Log("YouDied");
        //also disable button press? Probably will just have to check if other player near cannon
        playerSprite.color = new Color(1, 0, 0, 0.3f);//make player a ghost? 

    }
    
    private void ChangeExpressionOffset(float Offset)
    {
        materialInstance.mainTextureOffset = new Vector2(Offset, 0f);
    }

}
