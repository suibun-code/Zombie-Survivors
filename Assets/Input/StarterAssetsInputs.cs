using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class StarterAssetsInputs : MonoBehaviour
{
    [SerializeField] Camera mainCam;

    [Header("Character Input Values")]
    public Vector2Int mouseWorldPosInt;
    public Vector3Int screenMiddle;
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
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
    }

    public void OnInteract(InputValue value)
    {
        //SetMiddleOfScreenInWorldPos();
    }


    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


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

#if !UNITY_IOS || !UNITY_ANDROID

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void SetMouseWorldPosInt(Vector2 pos)
    {
        Vector2 temp = mainCam.ScreenToWorldPoint(look);
        mouseWorldPosInt = new Vector2Int((int)temp.x, (int)temp.y);
    }

    public void SetMiddleOfScreenInWorldPos()
    {
        Vector3 temp = mainCam.ScreenToWorldPoint(new Vector3((Screen.width / 2), (Screen.height / 2), mainCam.nearClipPlane));
        screenMiddle = new Vector3Int((int)temp.x, (int)temp.y, (int)temp.z);
    }

#endif

}