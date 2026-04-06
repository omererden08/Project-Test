using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UserInput : Singleton<UserInput>
{
    private PlayerInput _playerInput;

    [Header("Player")]
    private InputAction _moveAction;
    public InputAction MoveAction { get { return _moveAction; } }
    private InputAction _jumpAction;
    private InputAction _scaleIncreaseAction;
    private InputAction _changeColorAction;


    [Header("Device")]
    private InputDeviceType inputDeviceType = InputDeviceType.Keyboard;
    public InputDeviceType InputDeviceType { get { return inputDeviceType; } }

    [Header("Action Map")]
    private Input_ActionMap_Type actionMapType = Input_ActionMap_Type.Player;
    public Input_ActionMap_Type ActionMapType { get { return actionMapType; } }


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        SetInitials();
    }

    private void OnEnable()
    {
        #region Event
        EventManager<InputDeviceType>.EventRegister(EventKey.Input_DeviceUpdated, InputDeviceUpdated);
        EventManager<Input_ActionMap_Type>.EventRegister(EventKey.Input_ActionMap_Update, Input_ActionMap_Update);
        #endregion
    }
    private void OnDisable()
    {
        #region Event
        EventManager<InputDeviceType>.EventUnregister(EventKey.Input_DeviceUpdated, InputDeviceUpdated);
        EventManager<Input_ActionMap_Type>.EventUnregister(EventKey.Input_ActionMap_Update, Input_ActionMap_Update);
        #endregion
    }

    private void SetInitials()
    {
        if (_playerInput != null) return;

        _playerInput = GetComponent<PlayerInput>();
        SetupInputActions();
        _playerInput.ActivateInput();
    }

    #region Get
    public PlayerInput GetPlayerInput()
    {
        SetInitials();
        return _playerInput;
    }
    #endregion

    private void SetupInputActions()
    {
        #region Player
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _scaleIncreaseAction = _playerInput.actions["Player_ScaleIncrease"];
        _changeColorAction = _playerInput.actions["Player_ChangeColor"];

        _jumpAction.performed += ctx => InputAction_Jump(true);
        _jumpAction.canceled += ctx => InputAction_Jump(false);
        _scaleIncreaseAction.performed += ctx => InputAction_ScaleIncrease();
        _changeColorAction.performed += ctx => InputAction_ChangeColor();
        #endregion
    }

    #region Input Action Controls - Player
    private void InputAction_Jump(bool isPressed)
    {
        EventManager<bool>.EventTrigger(EventKey.Jump, isPressed);
    }
    private void InputAction_ScaleIncrease()
    {
        EventManager<bool>.EventTrigger(EventKey.Player_ScaleIncrease, true);
    }
    private void InputAction_ChangeColor()
    {
        EventManager<bool>.EventTrigger(EventKey.Player_ChangeColor_Request, true);
    }

    #endregion

    #region Control Enable/Disable
    public void ControlsEnable(bool state)
    {
        SetInitials();

        if (state)
            _playerInput.ActivateInput();
        else
            _playerInput.DeactivateInput();
    }
    #endregion

    #region Device Change
    private void InputDeviceUpdated(InputDeviceType deviceType)
    {
        inputDeviceType = deviceType;
    }
    #endregion

    #region Input Action Map
    private void Input_ActionMap_Update(Input_ActionMap_Type mapType)
    {
        actionMapType = mapType;

        switch (mapType)
        {
            case Input_ActionMap_Type.Player:
                _playerInput.SwitchCurrentActionMap("Player");
                break;
            default:
                break;
        }

        #region Event
        EventManager<Input_ActionMap_Type>.EventTrigger(EventKey.Input_ActionMap_Updated, mapType);
        #endregion
    }
    #endregion
}
