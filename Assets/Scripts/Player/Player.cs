using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Child")]
    [SerializeField] private SpriteRenderer sRenderer;

    [Header("Movement")]
    [SerializeField, Range(0, 100)] private float speed;


    private bool controlEnabled = true;
    public bool ControlEnabled { get { return controlEnabled; } }

    private Vector2 moveInput = Vector2.zero;
    private bool isJumpInput_Pressing;


    private UserInput _userInput;
    private UserInput userInput
    {
        get { if (_userInput == null) _userInput = UserInput.Instance; return _userInput; }
    }

    #region Enable/Disable
    private void OnEnable()
    {
        #region Event
        EventManager<bool>.EventRegister(EventKey.Jump, Movement_JumpInput);
        EventManager<bool>.EventRegister(EventKey.ControlEnabled_All, ControlEnabled_Set);
        EventManager<bool>.EventRegister(EventKey.Player_ScaleIncrease, ScaleIncrease);
        EventManager<Color>.EventRegister(EventKey.Player_ChangeColor_Apply, ChangeColor);
        #endregion
    }
    private void OnDisable()
    {
        #region Event
        EventManager<bool>.EventUnregister(EventKey.Jump, Movement_JumpInput);
        EventManager<bool>.EventUnregister(EventKey.ControlEnabled_All, ControlEnabled_Set);
        EventManager<bool>.EventUnregister(EventKey.Player_ScaleIncrease, ScaleIncrease);
        EventManager<Color>.EventUnregister(EventKey.Player_ChangeColor_Apply, ChangeColor);       
        #endregion
    }
    #endregion

    #region Update
    private void Update()
    {
        #region Input
        // Move
        Movement_UpdateInputValue();
        #endregion
    }
    private void FixedUpdate()
    {
        #region Movement
        Move();
        #endregion
    }
    #endregion

    #region Input
    #region Move
    private float Move_GetInput_Horizontal()
    {
        if (!ControlEnabled) return 0;

        return moveInput.x;
    }
    private void Move_SetInput(Vector2 _moveInput)
    {
        moveInput = _moveInput;
    }
    private void Movement_UpdateInputValue()
    {
        if (!ControlEnabled) return;

        Vector2 _moveInput = userInput.MoveAction.ReadValue<Vector2>();

        Move_SetInput(_moveInput);
    }
    #endregion

    #region Jump
    private void Movement_JumpInput(bool jumpInputPressed)
    {
        if (!ControlEnabled) return;

        if (jumpInputPressed)
        {
            JumpInput_State_Set(true);
            OnJumpInput();
        }
        else
        {
            JumpInput_State_Set(false);
            OnJumpUpInput();
        }
    }
    private void JumpInput_State_Set(bool state)
    {
        if (!ControlEnabled) return;

        isJumpInput_Pressing = state;
    }
    #endregion
    #endregion

    #region Control
    private void ControlEnabled_Set(bool state)
    {
        controlEnabled = state;
        
        if (!state)
            Move_SetInput(Vector2.zero);
    }
    #endregion

    #region Movement
    #region Move
    private void Move()
    {
        Vector3 moveValue = Vector3.right * Move_GetInput_Horizontal() * speed * Time.fixedDeltaTime;
        transform.Translate(moveValue, Space.World);
    }
    #endregion

    #region Jump
    /// <summary>Start player jump.</summary>
    private void OnJumpInput()
    {
        if (!ControlEnabled) return;

        // ::: TODO :::
    }
    /// <summary>Cancel player jump.</summary>
    private void OnJumpUpInput()
    {
        // ::: TODO :::
    }
    #endregion
    #endregion

    #region Scale
    /// <summary>Increase scale of the player.</summary>
    /// <param name="param">Not using.</param>
    private void ScaleIncrease(bool param)
    {
        transform.localScale = new Vector3(
            transform.localScale.x + .1f,
            transform.localScale.y + .1f,
            transform.localScale.z + .1f);
    }
    #endregion

    #region Color
    private void ChangeColor(Color newColor)
    {
        if (sRenderer == null) return;

        sRenderer.color = newColor;
    }
    #endregion



}
