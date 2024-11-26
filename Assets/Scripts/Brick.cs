using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject explosionPrefab;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.BricksOnLevel++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (gameManager != null)
        {
            gameManager.BricksOnLevel--;
        }

        SpawnPowerUp();
        Destroy(gameObject);
    }

    private void SpawnPowerUp()
    {
        int numeroRandom = Random.Range(0, 100);
        if (numeroRandom < 40) // 40% de probabilidad
        {
            GameObject newPowerUp = PowerUpPool.Instance.GetFromPool();
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = transform.position;
                newPowerUp.SetActive(true);
            }
        }
    }
}
