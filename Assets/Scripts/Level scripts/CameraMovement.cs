using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // set in Inspector
    public float movementSpeed;
    public float rotationSpeed;


    private bool isRightButtonDown = false;

    // Update is called once per frame
    void Update()
    {
        MoveCamera();

        SwitchMouseDown();

        RotateCamera();
    }

    private void MoveCamera()
    {
        Vector3 moveInput = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical"));

        if (moveInput.x != 0 || moveInput.z != 0)
        {
            transform.Translate(moveInput * Time.deltaTime * movementSpeed);
        }
    }

    private void SwitchMouseDown()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightButtonDown = true;
            oldMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightButtonDown = false;
        }
    }

    private Vector3 oldMousePos;

    private void RotateCamera()
    {
        if (!isRightButtonDown)
            return;

        if(oldMousePos.x != Input.mousePosition.x)
        {
            float rotationDirection = -Mathf.Sign(oldMousePos.x - Input.mousePosition.x);

            transform.Rotate(new Vector3(0, rotationDirection, 0) * rotationSpeed * Time.deltaTime);
        }


        oldMousePos = Input.mousePosition;
    }
}
