using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGen : MonoBehaviour
{
    public GameObject Cloud;
    public float width = 10;
    public float length = 10;
    public float height = 10;
    public float CloudSize = 10;
        
    void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                //for (int z = 0; z < height; z++)
                //{
                //    GameObject cloudobj = Instantiate(Cloud, new Vector3(transform.position.x + x * CloudSize, transform.position.y + z * CloudSize, transform.position.z + y * CloudSize), Quaternion.identity);
                //    cloudobj.name = "Cloud " + x + "_" + y + "_" + z;
                //    cloudobj.transform.SetParent(transform);
                //}

                GameObject cloudobj = Instantiate(Cloud, new Vector3(transform.position.x + x * CloudSize, transform.position.y, transform.position.z + y * CloudSize), Quaternion.identity);
                cloudobj.name = "Cloud " + x + "_" + y;
                cloudobj.transform.SetParent(transform); 
            }
        }
    }

   
}
