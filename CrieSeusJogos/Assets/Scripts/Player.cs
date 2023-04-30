using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimationController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private Vector2 _direction;
    private float _currentSpeed;
    private bool _isRunning;
    private bool _isRolling;

    private Dictionary<PlayerTriggerAnimationEnum, string> PlayerTriggerAnimation => new()
    {
        { PlayerTriggerAnimationEnum.Roll, "isRoll"}
    };

    private Rigidbody2D _rig;
    private AnimationController _animController;
    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _animController = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
        UpdateSpriteDirection();
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    private void HandleInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _currentSpeed = _walkSpeed;
        _isRunning = false;
        _isRolling = false;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = _runSpeed;
            _isRunning = true;
        }
        if (Input.GetMouseButtonDown(1))
            TriggerRolling();
    }

    private void TriggerRolling()
    {
        if (IsMoving)
            _animController.Trigger(PlayerTriggerAnimation[PlayerTriggerAnimationEnum.Roll]);
    }

    private void PerformMovement()
    {
        _rig.MovePosition(_rig.position + _direction * _currentSpeed * Time.fixedDeltaTime);
    }

    #region Animation / Sprite
    private void UpdateAnimationState()
    {
        if (IsMoving) _animController.SetAnimation(AnimationEnum.Anim_Walking);
        else _animController.SetAnimation(AnimationEnum.Anim_Idle);
        if (_isRunning) _animController.SetAnimation(AnimationEnum.Anim_Running);
    }

    private void UpdateSpriteDirection()
    {
        if(_direction.x != 0f)
            transform.eulerAngles = new Vector2(0, _direction.x > 0 ? 0 : 180);
    }
    #endregion

    private bool IsMoving => _direction.sqrMagnitude > 0f;
}

internal enum PlayerTriggerAnimationEnum
{
    Roll
}
