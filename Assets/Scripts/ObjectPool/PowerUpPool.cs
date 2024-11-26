using UnityEngine;
using UnityEngine.Pool;

public class PowerUpPool : MonoBehaviour
{
    public static PowerUpPool Instance;

    [SerializeField] GameObject[] powerUpPrefab;
    private ObjectPool<GameObject> powerUpPool;

    [SerializeField] Transform poolContainer;//En esta variable guardaremos los power ups, actuara como contenedor

    private void Awake()
    {
        Instance = this;
        /*Verificamos que PoolContainer este creado, en caso que no, crea uno nuevo*/
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
                /*Llevaremos los power ups al Game Object Pool container y se una vez llevado se los desactivara a cada uno*/
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
                powerUp.transform.position = Vector3.zero;//Iniciamos la posicion en 0
            },
            actionOnRelease: powerUp =>
            {
                powerUp.SetActive(false);//Una vez utilizado el power up lo desactivamos
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
    /*Activaremos el power up*/
    public GameObject GetFromPool()
    {
        int randomIndex = Random.Range(0, powerUpPrefab.Length);
        GameObject selectPowerUp = poolContainer.GetChild(randomIndex).gameObject;
        selectPowerUp.SetActive(true);

        // Asocia el efecto correspondiente al PowerUp
        var powerUpScript = selectPowerUp.GetComponent<PowerUp>();
        if (powerUpScript != null)
        {
            powerUpScript.SetEffect(randomIndex); // Asigna el efecto basado en el índice
        }

        return selectPowerUp;
    }

    /*Devolvera el power up al pool*/
    public void ReturnToPool(GameObject powerUp)
    {
        if (powerUp != null)
        {
            Debug.Log($"Power-Up {powerUp.name} desactivado y devuelto al pool");
            powerUpPool.Release(powerUp);
        }
    }
}