using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private Transform Transform_Player;
    [SerializeField] private float _detectRadius = 8f;
    [SerializeField] private float _attackRadius = 1.5f;

    [Header("Move")]
    [SerializeField] private float _moveSpeed = 2.5f;
    [SerializeField] private float _rotationSpeed = 360f;

    [Header("Attack")]
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackCooldown = 1.2f;

    private Animator _animator;
    private Health _health;
    private float _lastAttackTime = -999f;

    private string Anim_IsMoving = "IsMoving";
    private string Anim_Attack = "Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_health != null && _health.IsDead) return;
        if (Transform_Player == null) return;

        float distance = Vector3.Distance(transform.position, Transform_Player.position);

        if (distance <= _attackRadius)
        {
            _animator.SetBool(Anim_IsMoving, false);
            TryAttack();
        }
        else if (distance <= _detectRadius)
        {
            ChasePlayer();
        }
        else
        {
            _animator.SetBool(Anim_IsMoving, false);
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (Transform_Player.position - transform.position);
        direction.y = 0f;
        direction.Normalize();

        transform.position += direction * _moveSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _animator.SetBool(Anim_IsMoving, true);
    }
    
    private void TryAttack()
    {
        bool canAttack = Time.time >= _lastAttackTime + _attackCooldown;
        if (!canAttack) return;

        _lastAttackTime = Time.time;
        _animator.SetTrigger(Anim_Attack);

        Health playerHealth = Transform_Player.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
