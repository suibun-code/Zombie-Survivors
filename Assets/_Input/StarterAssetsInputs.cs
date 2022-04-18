using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class StarterAssetsInputs : MonoBehaviour
{
    [SerializeField] Camera mainCam;

    [Header("Character Input Values")]
    public Vector2 mouseWorldPos;
    public Vector2Int mouseWorldPosInt;
    public Vector3Int screenMiddle;
    public Vector3Int screenMidRaycast;
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool aim;
    public bool fire;
    public bool buildMode;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }

        SetMouseWorldPosInt(value.Get<Vector2>());
        SetMiddleOfScreenInWorldPos();
        SetMiddleOfScreenRayCast();
    }

    public void OnInteract(InputValue value)
    {

    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnAim(InputValue value)
    {
        AimInput(value.isPressed);
    }

    public void OnFire(InputValue value)
    {
        FireInput(value.isPressed);
    }

    public void OnBuildMode(InputValue value)
    {
        if (value.isPressed)
        {
            BuildModeInput();
        }
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void AimInput(bool newAimState)
    {
        aim = newAimState;
    }

    public void FireInput(bool newFireState)
    {
        fire = newFireState;
    }

    public void BuildModeInput()
    {
        if (buildMode)
            buildMode = false;
        else
            buildMode = true;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    public void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void SetMouseWorldPos()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = mainCam.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f))
            mouseWorldPos = hitInfo.point;
    }

    private void SetMouseWorldPosInt(Vector2 pos)
    {
        Vector2 temp = mainCam.ScreenToWorldPoint(look);
        mouseWorldPosInt = new Vector2Int((int)temp.x, (int)temp.y);
    }

    public void SetMiddleOfScreenInWorldPos()
    {
        Vector3 temp = mainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 6f));
        screenMiddle = new Vector3Int((int)temp.x, (int)temp.y, (int)temp.z);
    }

    public void SetMiddleOfScreenRayCast()
    {
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 temp = hitInfo.point;
            screenMidRaycast = new Vector3Int((int)temp.x, (int)temp.y, (int)temp.z);
            Debug.DrawRay(mainCam.transform.position, ray.direction, Color.red, 25f);
        }
    }
}