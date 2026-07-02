using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private float _rotationSpeed = 360f;

    [Header("Jump / Gravity")]
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _fallGravityMultiplier = 2.2f;

    [Header("Ground Check")]
    [SerializeField] private Transform Transform_GroundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _moveDirection;
    private float _currentSpeed;
    private bool _isGrounded;

    private string Anim_Speed = "Speed";
    private string Anim_IsGrounded = "IsGrounded";
    private string Anim_Jump = "Jump";
    private string Anim_IsRunning = "IsRunning";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        Vector2 input = InputManager.Instance.MoveInput;
        _moveDirection = new Vector3(input.x, 0f, input.y).normalized;

        float targetSpeed = 0f;
        if (_moveDirection.magnitude > 0.1f)
        {
            targetSpeed = InputManager.Instance.RunHeld ? _runSpeed : _walkSpeed;
        }
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.deltaTime * 10f);

        if (_moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        if (InputManager.Instance.JumpPressed && _isGrounded)
        {
            _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _animator.SetTrigger(Anim_Jump);
        }

        bool isRunning = InputManager.Instance.RunHeld && _moveDirection.magnitude > 0.1f;

        _animator.SetFloat(Anim_Speed, _currentSpeed);
        _animator.SetBool(Anim_IsGrounded, _isGrounded);
        _animator.SetBool(Anim_IsRunning, isRunning);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(Transform_GroundCheck.position, _groundCheckRadius, _groundMask);

        Vector3 horizontalVelocity = _moveDirection * _currentSpeed;
        _rigidbody.linearVelocity = new Vector3(horizontalVelocity.x, _rigidbody.linearVelocity.y, horizontalVelocity.z);

        if (_rigidbody.linearVelocity.y < 0f)
        {
            _rigidbody.linearVelocity += Vector3.up * Physics.gravity.y * (_fallGravityMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Transform_GroundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Transform_GroundCheck.position, _groundCheckRadius);
    }
}
