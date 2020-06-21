using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rigidbody;

    private bool shouldSpin = true;
    private int arrowDamage;

    public void Init(int arrowDamage)
    {
        this.arrowDamage = arrowDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldSpin)
            SpinObject();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.name.Contains("Archer") || collision.gameObject.name.Contains("Arrow")) // TODO : придумать более нормальный способ
            return;

        shouldSpin = false;
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;

        transform.parent = collision.transform;
        TryDealDamage(collision.gameObject.GetComponent<HealthBehaviour>());
    }

    private void TryDealDamage(HealthBehaviour targetHealth)
    {
        if (targetHealth == null)
            return;
        bool condition = false;

        targetHealth.TakeDamage(arrowDamage,  out condition);
    }

    
}
