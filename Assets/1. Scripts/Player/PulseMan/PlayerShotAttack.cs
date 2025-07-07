using UnityEngine;

public class PlayerShotAttack : MonoBehaviour
{
    public GameObject Bullet;              // 발사할 총알 프리팹
    public Transform AttackPoint;          // 총알이 나갈 위치
    public float bulletSpeed = 20f;        // 총알 속도
    public float fireRate = 0.2f;          // 연사 간격
    private float nextFireTime = 0f;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(Bullet, AttackPoint.position, AttackPoint.rotation);

        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = AttackPoint.forward * bulletSpeed;
        }
        playerController.isAttack = false;
    }
}
