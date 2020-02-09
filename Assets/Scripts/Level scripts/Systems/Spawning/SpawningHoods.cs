using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SpawningHoods : MonoBehaviour
{
    //public GameObject hoodPrefab; // TODO : просто сделать здесь ссылку на SO с нужным префабом, и менять его
    public GameObjSO hoodPrefab;
    public int maximumHoods;

    private int hoodsSpawned = 1;
    private bool canSpawn = true;

    protected const float maxDistanceFromNonWalkable = 10f;
    // Update is called once per frame
    void Update()
    {
        if (hoodPrefab.gameObject == null)
            return;

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
        
        if(Vector3.Distance(spawnPosition, Vector3.negativeInfinity) > 1)
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
        NavMeshHit hit;

        NavMesh.SamplePosition(spawnPosition, out hit, maxDistanceFromNonWalkable, NavMesh.AllAreas);

        #if DEBUG_MODE
        Debug.Log("Click position: " + spawnPosition);
        Debug.Log("Closest navMesh position: " + hit.position);
        #endif


        //hit.position.y += 0.8f;

        Vector3 toSpawn = new Vector3(hit.position.x, hit.position.y + 0.8f, hit.position.z);

        Instantiate(hoodPrefab.gameObject, toSpawn, Quaternion.identity, null);
    }

    public void TurnOffSpawning()
    {
        canSpawn = false;
    }
}
