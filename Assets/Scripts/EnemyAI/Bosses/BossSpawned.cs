using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossSpawned : MonoBehaviour
{
    public GameObject particlePrefab;
    public string nameBoss;

    private GameObject bossHealthBar;
    private CameraFollower cameraFollower;

    private void Awake()
    {
        cameraFollower = Camera.main.GetComponent<CameraFollower>();
        bossHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar");

        if (bossHealthBar != null)
        {
            Transform hpBar = bossHealthBar.transform.Find("HealthBarBoss");
            Transform bossName = bossHealthBar.transform.Find("Name");

            hpBar.gameObject.SetActive(true);
            bossName.gameObject.SetActive(true);

            HealthBarBoss namer = bossHealthBar.transform.Find("HealthBarBoss").GetComponent<HealthBarBoss>();

            if (namer != null)
            {
                namer.bossName = nameBoss; 
            }
        }

        // Disable camera following
        cameraFollower.SetCameraFollowing(false);

        StartCoroutine(ZoomInAndReset(gameObject));
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

    public void OnDead()
    {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }
}
