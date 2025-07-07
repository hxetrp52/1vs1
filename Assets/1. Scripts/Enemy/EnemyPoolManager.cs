using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPoolData
    {
        public string name;
        public GameObject prefab;
        public int poolSize = 10;

        public int hp = 30;
        public float minSpeed = 1f;
        public float maxSpeed = 3f;
    }

    public EnemyPoolData[] enemyTypes;

    private Dictionary<int, Queue<GameObject>> enemyPools = new();
    private Dictionary<int, EnemyPoolData> poolDataLookup = new();

    private void Awake()
    {
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            var data = enemyTypes[i];

            Queue<GameObject> pool = new();
            for (int j = 0; j < data.poolSize; j++)
            {
                GameObject obj = Instantiate(data.prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            enemyPools[i] = pool;
            poolDataLookup[i] = data;
        }
    }

    public GameObject GetEnemy(int index, Vector3 position, Quaternion rotation)
    {
        if (!enemyPools.ContainsKey(index))
        {
            Debug.LogError($"EnemyPoolManager: {index}번 풀 없음");
            return null;
        }

        var pool = enemyPools[index];
        var data = poolDataLookup[index];

        GameObject obj = (pool.Count > 0) ? pool.Dequeue() : Instantiate(data.prefab);

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        // HP 설정
        var health = obj.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.SetHP(data.hp);
        }

        // 이동 속도 설정
        var move = obj.GetComponent<EnemyMovement>();
        if (move != null)
        {
            move.SetSpeedRange(data.minSpeed, data.maxSpeed);
            move.SetRandomSpeed();
        }

        return obj;
    }

    public void ReturnEnemy(int index, GameObject obj)
    {
        obj.SetActive(false);
        enemyPools[index].Enqueue(obj);
    }
}
