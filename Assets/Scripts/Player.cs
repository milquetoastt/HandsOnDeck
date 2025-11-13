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

    public float fixedY = 0f;     // The Y height you want locked
    private float lockedRotX = 0f;
    private float lockedRotZ = 0f;

    private Material materialInstance;

    void Start()
    {
        alive = true;
        invisibility = false;
        materialInstance = faceRenderer.material;

        ChangeExpressionOffset(0.00f);
        haloRenderer.enabled = false;
        // Lock down the initial X/Z rotation from the starting transform
        lockedRotX = transform.rotation.eulerAngles.x;
        lockedRotZ = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        // --- 1. Freeze Y position ---
        Vector3 p = transform.position;
        p.y = fixedY;
        transform.position = p;

        // --- 2. Freeze X and Z rotation ---
        Vector3 e = transform.rotation.eulerAngles;
        e.x = lockedRotX;
        e.z = lockedRotZ;
        transform.rotation = Quaternion.Euler(e);
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
