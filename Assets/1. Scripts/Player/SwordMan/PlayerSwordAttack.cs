using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float attackRadius = 1f;
    public int attackDamage = 10;
    public ParticleSystem[] slashParticle;
    private PlayerController playerController;
    public Transform attackPoint; // 검 끝 위치


    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void PerformAttack()
    {
        // 근접 범위 체크
        slashParticle[0].Play();
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRadius);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // 적에게 데미지 전달
                enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
            }
        }
        playerController.isAttack = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
