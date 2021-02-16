using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private GameSession _gameSession;
    
    private void DecreasePlayerHealth(int damageAmount)
    {
        health -= damageAmount;
        _gameSession.DecreasePlayerHealthBy(damageAmount);

        if (health <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneLoader.LoadScene("Death");
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyComponent = other.GetComponent<Enemy>();

        if (!enemyComponent) return;
        
        DecreasePlayerHealth(enemyComponent.Damage);
        Destroy(other.gameObject);
        WaveSpawner.DecreaseAliveEnemyCount();
    }
}
