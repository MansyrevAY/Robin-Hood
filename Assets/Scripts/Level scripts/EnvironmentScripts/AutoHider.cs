using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHider : MonoBehaviour
{

    public GameObject worldObj;
    public float desiredDistance = 8;
    public float maxDistance = 100;
    public bool doShow = true;
    public bool doScale = false;

    
    void Update()
    {
        dissapearCheck();
    }

    private void dissapearCheck()
    {
        foreach (Transform child in worldObj.transform)
        {
            float distance = Vector3.Distance(child.position, transform.position);

            if(distance < desiredDistance)
            {
                if (!doShow)
                {
                    if (!child.gameObject.activeInHierarchy)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if(child.gameObject.activeInHierarchy && !doScale)
                    {
                        child.gameObject.SetActive(false);
                    }
                    else if(child.gameObject.activeInHierarchy && doScale)
                    {
                        child.GetComponent<Scaler>().ToggleScale(false);
                    }
                }
            }
            else
            {
                if (!doShow)
                {
                    if (child.gameObject.activeInHierarchy)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if(distance < maxDistance)
                    {
                        if (!child.gameObject.activeInHierarchy && !doScale)
                        {
                            child.gameObject.SetActive(true);
                        }
                        else if (!child.gameObject.activeInHierarchy && doScale)
                        {
                            child.gameObject.SetActive(true);
                            child.GetComponent<Scaler>().ToggleScale(true);
                        }
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
