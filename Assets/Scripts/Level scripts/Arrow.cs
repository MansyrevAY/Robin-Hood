using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rigidbody;

    private bool shouldSpin = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(rigidbody.velocity);
        if(shouldSpin)
            SpinObject();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.name.Contains("Archer")) // TODO : придумать более нормальный способ
            return;

        shouldSpin = false;
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;

    }

    void SpinObject()
    {
        float _yVelocity = rigidbody.velocity.y;
        float _zVelocity = rigidbody.velocity.z;
        float _xVelocity = rigidbody.velocity.x;
        float combinedVelocity = Mathf.Sqrt(_xVelocity * _xVelocity + _zVelocity * _zVelocity);
        float fallAngle = -1 * Mathf.Atan2(_yVelocity, combinedVelocity) * 180 / Mathf.PI;

        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
