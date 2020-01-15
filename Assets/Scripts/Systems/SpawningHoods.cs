using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningHoods : MonoBehaviour
{
    public GameObject hoodPrefab;
    public int maximumHoods;
    private int hoodsSpawned = 1;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && hoodsSpawned <= maximumHoods)
        {
            ShootRayAtMouse();
        }
    }

    private void ShootRayAtMouse()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        
        if(spawnPosition != Vector3.negativeInfinity)
        {
            SpawnHoodAt(spawnPosition);
            hoodsSpawned++;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Ray ray;
        RaycastHit[] hitInfo;
        Vector3 resultPosition;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hitInfo = Physics.RaycastAll(ray, 150);

        if (hitInfo.Length > 1 || hitInfo[0].transform.name != "Terrain")
            resultPosition = Vector3.negativeInfinity;
        else
            resultPosition = hitInfo[0].point;

        return resultPosition;
    }

    private void SpawnHoodAt(Vector3 spawnPosition)
    {
        spawnPosition.y += 0.8f;

        Instantiate(hoodPrefab, spawnPosition, Quaternion.identity, null);
    }
}
