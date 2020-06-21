using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggler : MonoBehaviour
{
    public float maxSize;
    public float minSize;
    public float maxScaleSpeed;
    public float minScaleSpeed;

    private float desiredSize;
    private float desiredSpeed;

    void Start()
    {
        GenerateNewSize();
    }

    void Update()
    {
        if(desiredSize < transform.localScale.x)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * desiredSpeed, transform.localScale.y - Time.deltaTime * desiredSpeed, transform.localScale.z - Time.deltaTime * desiredSpeed);

            if(transform.localScale.x <= desiredSize)
            {
                GenerateNewSize();
            }
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * desiredSpeed, transform.localScale.y + Time.deltaTime * desiredSpeed, transform.localScale.z + Time.deltaTime * desiredSpeed);

            if(transform.localScale.x >= desiredSize)
            {
                GenerateNewSize();
            }
        }
    }

    private void GenerateNewSize()
    {
        desiredSize = Random.Range(minSize, maxSize);
        desiredSpeed = Random.Range(minScaleSpeed, maxScaleSpeed);
    }
}
