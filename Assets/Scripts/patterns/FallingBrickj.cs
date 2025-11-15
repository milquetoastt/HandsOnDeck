using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBrickj : MonoBehaviour
{

    public float fallSpeed = 10f;
    public float startHeight = 20f;
    public float groundY = 0f;
    public GameObject explosionPrefab;
    public float destroyDelay = 0.05f;

    public Transform shadowTransform;
    public Renderer shadowRenderer; // works with 3D plane or quad
    public float maxShadowScale = 2.5f;
    public float minShadowScale = 0.5f;
    public float maxShadowAlpha = 0.8f;
    public float minShadowAlpha = 0.1f;
    public float shadowOffsetY = 0.02f; // small lift above ground to prevent z-fighting

    private float startY;
    private Material shadowMatInstance;
    private Vector3 shadowBaseScale;

    void Start()
    {
        // start above ground
        startY = transform.position.y;
        transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);

        // get a unique material instance so each shadow can change alpha independently

        if (shadowRenderer != null)
            shadowMatInstance = shadowRenderer.material;

        if (shadowTransform != null)
            shadowBaseScale = shadowTransform.localScale; // store the prefab’s shadow scale
    }

    void Update()
    {
        // move brick downward
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // stop at ground level
        if (transform.position.y <= groundY)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            Instantiate(explosionPrefab, transform.position, Quaternion.LookRotation(Vector3.up));

            //Destroy(gameObject, destroyDelay);
            // optional: small delay before cleanup (so you can see it land)
            StartCoroutine(DestroyAfterDelay(0.1f));

            // disable update so it doesn’t keep running
            enabled = false;
        }

        UpdateShadow();
    }

    void UpdateShadow()
    {
        if (shadowTransform == null || shadowRenderer == null) return;

        // get 0  1 range as brick falls
        float t = Mathf.InverseLerp(startHeight, groundY, transform.position.y);

        // scale shadow as it gets closer, factoring in brick's own size
        float scaleFactor = Mathf.Lerp(minShadowScale, maxShadowScale, t);
        shadowTransform.localScale = shadowBaseScale * scaleFactor;

        // darken as it gets closer
        float alpha = Mathf.Lerp(minShadowAlpha, maxShadowAlpha, t);
        if (shadowMatInstance != null)
        {
            Color c = shadowMatInstance.color;
            c.a = alpha;
            shadowMatInstance.color = c;
        }

        // position shadow directly below the brick, slightly above ground to avoid z-fighting
        shadowTransform.position = new Vector3(
            transform.position.x,
            groundY + shadowOffsetY,
            transform.position.z
        );
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // destroy shadow first (if assigned)
        if (shadowTransform != null)
            Destroy(shadowTransform.gameObject);

        // destroy the brick itself
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    Debug.Log("Brick hit something", collision);
    //    Player player = collision.gameObject.GetComponent<Player>();
    //    if (player != null)
    //    {
    //        //Debug.Log("player ran into death spot");
    //        player.Die();
    //    }
    //}
}