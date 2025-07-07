using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 30;
    private int currentHP;

    void OnEnable()
    {
        currentHP = maxHP;
    }

    public void SetHP(int hp)
    {
        maxHP = hp;
        currentHP = hp;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"{gameObject.name} 피격! 남은 체력: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} 사망");
        gameObject.SetActive(false); // Destroy 대신 풀로 반환하는 구조일 때
    }
}
