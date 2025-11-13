using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Cannon))]
public class CannonAimer : MonoBehaviour
{
    public Transform target;  // The position you want to hit
    public bool preferHighArc = false;
    public int numLane;

    private Cannon cannon;

    void Awake()
    {
        cannon = GetComponent<Cannon>();
    }

    void Update()
    {
        float effectiveSpeed = cannon.launchVelocity * Time.fixedDeltaTime / cannon.projectile.GetComponent<Rigidbody>().mass;
        if (target == null || cannon.firepoint == null) return;

        // Solve for the direction the firepoint should face
        Vector3 start = cannon.firepoint.position;
        Vector3 end = target.position;

        if (SolveAimRotation(start, end, cannon.launchVelocity / cannon.projectile.GetComponent<Rigidbody>().mass, Physics.gravity.y, preferHighArc, out Quaternion aimRot))
        {
            cannon.firepoint.rotation = aimRot;
        }
    }

    /// <summary>
    /// Compute rotation so AddRelativeForce(x=launchVelocity) hits target with gravity.
    /// </summary>
    private bool SolveAimRotation(Vector3 start, Vector3 target, float speed, float gravityY, bool highArc, out Quaternion rot)
    {
        rot = Quaternion.identity;
        Vector3 toTarget = target - start;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0, toTarget.z);

        float x = toTargetXZ.magnitude;
        float y = toTarget.y;
        float g = -gravityY; // make positive

        float v2 = speed * speed;
        float underRoot = v2 * v2 - g * (g * x * x + 2 * y * v2);

        if (underRoot < 0)
            return false; // no solution at this speeds

        float root = Mathf.Sqrt(underRoot);
        float low = Mathf.Atan((v2 - root) / (g * x));
        float high = Mathf.Atan((v2 + root) / (g * x));
        float angle = highArc ? high : low;

        Vector3 dir = toTargetXZ.normalized;
        Vector3 launchDir = Quaternion.AngleAxis(Mathf.Rad2Deg * -angle, Vector3.Cross(Vector3.up, dir)) * dir;

        rot = Quaternion.LookRotation(launchDir) * Quaternion.Euler(0, -90f,0);
        return true;
    }

    public int GetNumberLane()
    {
        return numLane; 
    }


}