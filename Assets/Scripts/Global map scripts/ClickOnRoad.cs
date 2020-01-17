using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnRoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootRayAtMouse();
        }
    }

    private void ShootRayAtMouse()
    {

        OpenNewLevel opener = GetLevelToOpen();

        if (opener != null)
            opener.StartLevel();
    }

    private Vector3 mousepos;
    private Vector2 mousePos2D;
    private RaycastHit2D[] hit;

    private OpenNewLevel GetLevelToOpen()
    {
        OpenNewLevel result = null ;

        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2D = new Vector2(mousepos.x, mousepos.y);

        hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

        if (hit.Length == 0)
            return null;

        result = hit[0].transform.gameObject.GetComponent<OpenNewLevel>();


        return result;
    }
}
