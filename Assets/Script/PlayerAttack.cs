using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private Transform Transform_AttackPoint;
    [SerializeField] private float _attackRadius = 1.2f;
    [SerializeField] private int _attackDamage = 20;
    [SerializeField] private float _attackCooldown = 0.6f;
    [SerializeField] private LayerMask _enemyMask;

    private Animator _animator;
    private Health _health;
    private float _lastAttackTime = -999f;

    private string Anim_Attack = "Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_health != null && _health.IsDead) return;

        bool canAttack = Time.time >= _lastAttackTime + _attackCooldown;

        if (InputManager.Instance.AttackPressed && canAttack)
        {
            PerformAttack();
            _lastAttackTime = Time.time;
        }
    }

    private void PerformAttack()
    {
        _animator.SetTrigger(Anim_Attack);

        Collider[] hits = Physics.OverlapSphere(Transform_AttackPoint.position, _attackRadius, _enemyMask);
        foreach (Collider hit in hits)
        {
            Health targetHealth = hit.GetComponentInParent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(_attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Transform_AttackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Transform_AttackPoint.position, _attackRadius);
    }
}
