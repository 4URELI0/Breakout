using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpObject;
    private Queue<GameObject> availablePowerUp = new Queue<GameObject>();
    public static PowerUpPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void GrowPool()
    {

        foreach (var powerUp in powerUpObject)
        {
            var instanceToPool = Instantiate(powerUp, Vector3.zero, Quaternion.identity);
            instanceToPool.SetActive(false);
            AddToPool(instanceToPool);
        }
    }
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availablePowerUp.Enqueue(instance);
    }
    public GameObject GetFromPool()
    {
        if (availablePowerUp.Count == 0)
        {
            GrowPool();
        }
        var instance = availablePowerUp.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}