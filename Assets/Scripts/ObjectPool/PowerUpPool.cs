using UnityEngine;
using UnityEngine.Pool;

public class PowerUpPool : MonoBehaviour
{
    public static PowerUpPool Instance;

    [SerializeField] GameObject[] powerUpPrefab;
    private ObjectPool<GameObject> powerUpPool;

    [SerializeField] Transform poolContainer;

    private void Awake()
    {
        Instance = this;
        if (poolContainer == null)
        {
            poolContainer = new GameObject("PowerUp_Pool_Container").transform;
        }
    }

    private void Start()
    {
        powerUpPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject powerUp = null;
                for (int i = 0; i < powerUpPrefab.Length; i++)
                {
                    powerUp = Instantiate(powerUpPrefab[i]);
                    powerUp.transform.SetParent(poolContainer);
                    powerUp.SetActive(false);
                }
                return powerUp;
            },
            actionOnGet: powerUp =>
            {
                powerUp.SetActive(true);
                // Opcional: Configurar posición inicial
                powerUp.transform.position = Vector3.zero;
            },
            actionOnRelease: powerUp =>
            {
                powerUp.SetActive(false);
                powerUp.transform.SetParent(poolContainer.transform);
                powerUp.transform.position = Vector3.zero;
            },
            collectionCheck: false,
            defaultCapacity: 1,
            maxSize: 2
        );
        for (int i = 0; i < 2; i++)
        {
            GameObject powerUp = powerUpPool.Get();
            powerUpPool.Release(powerUp);
        }
    }
    public GameObject GetFromPool()
    {
        return powerUpPool.Get();
    }
    public void ReturnToPool(GameObject powerUp)
    {
        if (powerUp != null)
        {
            Debug.Log($"Power-Up {powerUp.name} desactivado y devuelto al pool");
            powerUpPool.Release(powerUp);
        }
    }
}