using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 = TOP --> need BOTTOM door
    // 2 = BOTTOM --> need TOP door
    // 3 = LEFT --> need RIGHT door
    // 4 = RIGHT --> need LEFT door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;

    void Awake()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }
    void Spawn()
    {
        if (spawned == false) {
            if (openingDirection == 1)
            {
                //Spawn BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);

            }
            else if (openingDirection == 2)
            {
                //Spawn TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);

            }
            else if (openingDirection == 3)
            {
                //Spawn LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);

            }
            else if (openingDirection == 4)
            {
                //Spawn RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);

            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            RoomSpawner roomSpawner = other.GetComponent<RoomSpawner>();
            if (roomSpawner != null && roomSpawner.spawned == false && spawned == false)
            {
                templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
                // spawn walls blocking off openings
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
