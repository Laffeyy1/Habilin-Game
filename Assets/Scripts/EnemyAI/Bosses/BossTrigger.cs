using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject barrier;
    public GameObject bossPrefab;
    public Transform bossSpawnLoc; // Use Transform for the boss spawn location
    public string bossName;

    private GameObject bossHealthBar;
    private CameraFollower cameraFollower;

    private void OnTriggerEnter2D(Collider2D other)
    {
        cameraFollower = Camera.main.GetComponent<CameraFollower>();
        bossHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar");
        // Check if the player has entered the trigger
        if (other.CompareTag("Player"))
        {
            // Spawn the boss at the bossSpawnLoc position and rotation
            GameObject bossObject = Instantiate(bossPrefab, bossSpawnLoc.position, bossSpawnLoc.rotation);
            Animator animator = bossObject.GetComponent<Animator>();

            if (animator != null)
            {
                Boss_Run bossRunScript = animator.GetBehaviour<Boss_Run>();
                if (bossRunScript != null)
                {
                    // Disable player movement or any other necessary game mechanics here

                    // Activate the barrier if needed
                    barrier.SetActive(true);

                    if (bossHealthBar != null)
                    {
                        Transform hpBar = bossHealthBar.transform.Find("HealthBarBoss");
                        Transform bossName = bossHealthBar.transform.Find("Name");

                        hpBar.gameObject.SetActive(true);
                        bossName.gameObject.SetActive(true);
                    }

                    // Disable camera following
                    cameraFollower.SetCameraFollowing(false);

                    // Call a coroutine to handle the zoom and reset
                    StartCoroutine(ZoomInAndReset(bossObject));
                }
            }
        }
    }

    private IEnumerator ZoomInAndReset(GameObject bossObject)
    {
        // Zoom in to the boss
        // Adjust the camera size and position as needed
        float targetSize = 5f; // Adjust this to your desired zoom level
        Vector3 targetPosition = bossObject.transform.position;

        Camera camera = Camera.main; // Get the main camera component

        float initialSize = camera.orthographicSize;
        Vector3 initialPosition = camera.transform.position;

        float zoomSpeed = 2f; // Adjust the speed of zoom-in

        // Smoothly interpolate the camera size and position
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * zoomSpeed;
            camera.orthographicSize = Mathf.Lerp(initialSize, targetSize, elapsedTime);
            camera.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
            yield return null;
        }

        // Optionally, you can perform any actions or animations specific to the boss here

        // Delay for a few seconds (you can adjust the time as needed)
        yield return new WaitForSeconds(3f);

        // Reset the camera to its original position and size
        // You should adjust the original camera settings according to your game
        cameraFollower.SetCameraFollowing(true);

    }
}
