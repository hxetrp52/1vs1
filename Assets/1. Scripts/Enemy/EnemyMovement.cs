using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    private float maxSpeed;
    private float minSpeed;

    public void SetSpeedRange(float min, float max)
    {
        minSpeed = min;
        maxSpeed = max;
    }

    public void SetRandomSpeed()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Arcade")
        {
            Debug.Log("포탈에 닿았습니다!");
        }
    }
}
