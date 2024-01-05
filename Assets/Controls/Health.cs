using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemDrop
{
    public GameObject itemPrefab;
    [Range(0, 1)]
    public float dropProbability;
    public int minAmount;
    public int maxAmount;
}

public class Health : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    public int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    private bool isDead = false;

    private HealthBar healthBar;
    private HealthBarBoss healthBarBoss;

    [SerializeField]
    private ItemDrop[] itemDrops;

    [SerializeField] private GameObject deathParticle;

    GameObject gameOverPanel;
    private int difficultyMultiplier = 1;
    private int levelMultiplier = 1;
    private int lvlAdd;

    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficultyMultiplier = PlayerPrefs.GetInt("Difficulty");
            levelMultiplier = PlayerPrefs.GetInt("Level");
            lvlAdd = PlayerPrefs.GetInt("PlayerLevel");
        }

        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        if (CompareTag("Player"))
        {
            // Find the HealthBar in the scene
            healthBar = FindObjectOfType<HealthBar>();
            if (healthBar != null)
            {
                currentHealth += lvlAdd;
                maxHealth += lvlAdd;
                healthBar.SetMaxHealth(maxHealth);
            }
        }
        else if (CompareTag("Boss"))
        {
            healthBarBoss = FindObjectOfType<HealthBarBoss>();
            if (healthBarBoss != null)
            {
                currentHealth += difficultyMultiplier + levelMultiplier;
                maxHealth += difficultyMultiplier + levelMultiplier;
                healthBarBoss.SetMaxHealth(maxHealth);
            }
        }
        else if (CompareTag("Enemy"))
        {
            currentHealth += difficultyMultiplier + levelMultiplier;
            maxHealth += difficultyMultiplier + levelMultiplier;
        }
    }

    public void LoadData(GameData data)
    {
        lvlAdd = data.lapuLvl;
    }

    public void SaveData(GameData data)
    {

    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue + difficultyMultiplier;
        maxHealth = healthValue + difficultyMultiplier;
        isDead = false;
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        audioManager.GetHit();
        currentHealth -= amount;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
            // Update the health bar if it's not null
            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth, maxHealth);
            }
            else if(healthBarBoss != null){
                healthBarBoss?.SetHealth(currentHealth);
            }
        }
        else
        {
            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth, maxHealth);
                if (PlayerPrefs.HasKey("Mode"))
                {
                    string mode = PlayerPrefs.GetString("Mode");
                    if (mode == "Story")
                    {
                        gameOverPanel.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else if (mode == "Endless")
                    {
                        gameOverPanel.transform.GetChild(0).gameObject.SetActive(true);
                        GameObject leaderboard = GameObject.FindGameObjectWithTag("Leaderboard").transform.Find("RegisterLeaderboard").GameObject();
                        leaderboard.SetActive(true);
                    }
                }
            }
            else if (healthBarBoss != null)
            {
                healthBarBoss.SetHealth(currentHealth);
            }

            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            DropItems();
            Destroy(gameObject);
        }
    }

    private void DropItems()
    {
        if (itemDrops != null && itemDrops.Length > 0)
        {
            foreach (var itemDrop in itemDrops)
            {
                if (Random.value <= itemDrop.dropProbability)
                {
                    int amountToDrop = Random.Range(itemDrop.minAmount, itemDrop.maxAmount + 1);

                    for (int i = 0; i < amountToDrop; i++)
                    {
                        // Instantiate the chosen item prefab
                        GameObject droppedItem = Instantiate(itemDrop.itemPrefab, transform.position, Quaternion.identity);

                        // Apply a force to the dropped item (e.g., if it's an item that should fall)
                        Rigidbody2D itemRigidbody = droppedItem.GetComponent<Rigidbody2D>();
                        if (itemRigidbody != null)
                        {
                            // Adjust this force as needed
                            itemRigidbody.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
                        }
                    }
                }
            }
        }
        Instantiate(deathParticle, transform.position, Quaternion.identity);
    }

    public void AddHealth(int amount)
    {
        maxHealth += amount;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure health doesn't exceed max health.
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

}
