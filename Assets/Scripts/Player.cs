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
        alive = true;
        playerSprite = GetComponent<SpriteRenderer>();
        materialInstance = faceRenderer.material;

        ChangeExpressionOffset(0.00f);
        haloRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void RespawnPlayer() //StartCoroutine(RespawnPlayer());
    {
        Debug.Log("Player Respawn");

        ChangeExpressionOffset(0f);
        playerSprite.color = new Color(1, 0, 0, 1);
        haloRenderer.enabled = false;
        alive = true;
    }

    public void Die()
    {
        Debug.Log("YouDied");

        ChangeExpressionOffset(0.25f);
        haloRenderer.enabled = true;
    }
    
    private void ChangeExpressionOffset(float Offset)
    {
        materialInstance.mainTextureOffset = new Vector2(Offset, 0f);
    }

}
