using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawningHoods : MonoBehaviour
{
    public GameObject hoodPrefab;
    public int maximumHoods;

    private int hoodsSpawned = 1;
    private bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && hoodsSpawned <= maximumHoods && canSpawn)
        {
            ShootRayAtMouse();
        }
    }

    private void ShootRayAtMouse()
    {
        if (UIClicked())
            return;

        Vector3 spawnPosition = GetSpawnPosition();
        
        if(spawnPosition != Vector3.negativeInfinity)
        {
            SpawnHoodAt(spawnPosition);
            hoodsSpawned++;
        }
    }

    /// <summary>
    /// Проверка на попадание в интерфейс
    /// </summary>
    /// <returns>true, если попали в интерфейс</returns>
    private bool UIClicked()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult res in results)
        {
            if (res.gameObject.layer == 5)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Cast ray to the floor, returns -infinity if there is anything exept Terrain
    /// </summary>
    /// <returns></returns>
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

    public void TurnOffSpawning()
    {
        canSpawn = false;
    }
}
