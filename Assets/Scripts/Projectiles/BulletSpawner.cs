using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Attributes")]
    public GameObject bulletPrefab;
    public float bulletLife = 1f;
    public float speed = 1f;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private int bulletCount = 3; // Number of bullets to fire simultaneously

    private GameObject[] spawnedBullets;
    private Transform player;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnedBullets = new GameObject[bulletCount];
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
    }

    private void Fire()
    {
        if (bulletPrefab && player)
        {
            Vector3 playerDirection = player.position - transform.position;
            float angleToPlayer = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

            for (int i = 0; i < bulletCount; i++)
            {
                float angleOffset = i * (360f / bulletCount); // Calculate angle between bullets
                float bulletAngle = angleToPlayer + angleOffset;

                Vector3 spawnDirection = Quaternion.Euler(0f, 0f, bulletAngle) * Vector3.up;

                spawnedBullets[i] = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                spawnedBullets[i].GetComponent<Bullet>().speed = speed;
                spawnedBullets[i].GetComponent<Bullet>().bulletLife = bulletLife;

                // Set the direction for the bullet
                spawnedBullets[i].GetComponent<Bullet>().SetDirection(spawnDirection);
            }
        }
    }
}

