using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIShoot
{
    /// <summary>
    /// Returns Vector3 point of interception of projectile and target
    /// </summary>
    /// <param name="shooter"></param>
    /// <param name="target"></param>
    /// <param name="shotSpeed"></param>
    /// <returns></returns>
    public static Vector3 AimAtTarget(GameObject shooter, GameObject target, float shotSpeed)
    {

        // === derived variables ===
        //positions
        Vector3 shooterPosition = shooter.transform.position;
        Vector3 targetPosition = target.transform.position;
        //velocities
        Vector3 shooterVelocity = shooter.GetComponent<Rigidbody>() ? shooter.GetComponent<Rigidbody>().velocity : Vector3.zero;     
        Vector3 targetVelocity = target.GetComponent<Rigidbody>() ? target.GetComponent<Rigidbody>().velocity : Vector3.zero;

        //calculate intercept
        Vector3 interceptPoint = FirstOrderIntercept
        (
            shooterPosition,
            shooterVelocity,
            shotSpeed,
            targetPosition,
            targetVelocity
        );
        return interceptPoint;
    }

    //first-order intercept using absolute target position
    private static Vector3 FirstOrderIntercept(Vector3 shooterPosition, Vector3 shooterVelocity, float shotSpeed, Vector3 targetPosition, Vector3 targetVelocity)
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetPosition + t * (targetRelativeVelocity);
    }
    //first-order intercept using relative target position
    private static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }
}
