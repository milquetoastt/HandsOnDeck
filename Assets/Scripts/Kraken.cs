using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Kraken : MonoBehaviour
{
    public Transform[] points;
    //THE ABOVE LINE ONLY WORKS WITH 3 POINTS PLSPLSPLS DON'T ADD MOREIT BREAKS THINGS
    public float duration = 2f;
    public Transform target;
    public float bobAmplitude = 0.2f;
    public float bobSpeed = 2f;
    private float bobTimer = 0f;

    void Start()
    {
        StartCoroutine(PingPongPoints());
    }

    void RotateTowardTarget()
    {
        // Direction on the XZ plane only
        Vector3 dir = target.position - transform.position;
        dir.y = 0; // ignore vertical difference

        if (dir.sqrMagnitude < 0.001f) return;

        Quaternion lookRot = Quaternion.LookRotation(dir);

        // Only apply Y rotation
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            lookRot.eulerAngles.y,
            transform.rotation.eulerAngles.z
        );
    }


    IEnumerator PingPongPoints()
    {
        int index = 0;
        int direction = 1;

        while (true)
        {
            Vector3 start = points[index].position;
            Vector3 end = points[index + direction].position;

            yield return Move(start, end, duration);

            index += direction;

            // reverse
            if (index == 2 || index == 0)
                direction *= -1;
        }
    }

    IEnumerator Move(Vector3 start, Vector3 end, float time)
    {
        float t = 0f;

        while (t < time)
        {
            float normalized = t / time;
            float eased = EaseInOutCubic(normalized);

            // Base movement (non-bobbing)
            Vector3 basePos = Vector3.Lerp(start, end, eased);

            // Update bob timer
            bobTimer += Time.deltaTime * bobSpeed;

            // Bobbing offset
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;

            // Apply bobbing only on Y
            transform.position = new Vector3(
                basePos.x,
                basePos.y + bobOffset,
                basePos.z
            );

            // Rotation (only Y rotates)
            RotateTowardTarget();

            t += Time.deltaTime;
            yield return null;
        }

        // Final position with bobbing reset at end point
        transform.position = end;
        RotateTowardTarget();
    }

    float EaseInOutCubic(float x)
    {
        return (x < 0.5f)
            ? 4f * x * x * x
            : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
    }
}
