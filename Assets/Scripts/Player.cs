using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public bool alive;
    public bool invisibility;

    [SerializeField] private SkinnedMeshRenderer faceRenderer;
    [SerializeField] private SkinnedMeshRenderer haloRenderer;

    private Material materialInstance;

    void Start()
    {
        alive = true;
        invisibility = false;
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
        haloRenderer.enabled = false;
        alive = true;

        StartCoroutine(InvincibilityFrames());
    }

    public void Die()
    {
        if (!invisibility)
        {
            Debug.Log("YouDied");

            ChangeExpressionOffset(0.25f);
            haloRenderer.enabled = true;
            alive = false;
        }
    }

    private void ChangeExpressionOffset(float Offset)
    {
        materialInstance.mainTextureOffset = new Vector2(Offset, 0f);
    }

    public IEnumerator InvincibilityFrames()
    {
        Debug.Log("Player is invinsible");
        invisibility = true;
        yield return new WaitForSeconds(2);
        invisibility = false;
    }
}
