using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPoolManager poolManager;

    public float spawnInterval = 0.2f;
    private float timer;

    public float spawnXMin = -10f;
    public float spawnXMax = 10f;
    public float fixedY = 0f;
    public float fixedZ = 20f;

    public int enemyTypeIndex = 0; // 테스트용, 인덱스 지정 가능

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPos = new Vector3(randomX, fixedY, fixedZ);

        // Y축 180도 회전된 Quaternion 생성
        Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);

        poolManager.GetEnemy(enemyTypeIndex, spawnPos, rotation);
    }

}
