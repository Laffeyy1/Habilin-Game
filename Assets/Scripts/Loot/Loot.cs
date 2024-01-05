using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Loot : MonoBehaviour
{
    // You can chose to use Scriptable Object or Serialized.
    // One settings for SplashSetting is already created
    // [SerializeField] SplashSettings settings;	// Scriptable Object
    [SerializeField] LootSettings settings;         // Serialized

    private int bounces = 0;
    private bool isGrounded = true;
    private Vector2 groundVelocity;
    private float verticalVelocity, afterVelocity;
    private bool collide;

    private Transform t_parent; // Main
    private Transform t_body; // Body
    private Transform t_shadow; // Shadow

    private Transform playerTransform;
    private bool isFollowingPlayer = false;

    [SerializeField] LibrarySaveList librarySaveList;
    private SaveLibraryManager libraryManager;

    #region OPTIONAL PICK UP
    bool canCollect;

    BoxCollider2D pickUpCollision;
    // Detect if hits the wall / even it looks like its never used, it is.
    BoxCollider2D triggerCollision;

    // Dont forget to add rigidbody to the player and right Tag
    private void PickUp(Collider2D collision)
    {
        toSaveLibrary();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(settings.collectorTag) && canCollect)
            PickUp(collision);

        if (CompareCollisionTags(collision))
            collide = true;
    }

    #endregion

    void Awake()
    {
        libraryManager = GameObject.FindGameObjectWithTag("Library").GetComponent<SaveLibraryManager>();
        pickUpCollision = GetComponent<BoxCollider2D>();
        pickUpCollision.enabled = false;
        // Sprite has to be disabled, its sprite is only for t_body sprite
        GetComponent<SpriteRenderer>().enabled = false;
    }


    void Start()
    {
        var colliders = GetComponents<BoxCollider2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag(settings.collectorTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure to set the correct collectorTag or assign the player's transform in the Inspector.");
        }

        if (colliders.Length < 2)
        {
            Debug.LogError($"Loot {gameObject.name} require atleast 2 BoxCollider2Ds.");
            return;
        }

        colliders.All(col => col.isTrigger = true);

        if (settings.destroyTime > 0)
            Destroy(this.gameObject, settings.destroyTime);

        CreateBody();
        CreateShadow();
        SimulateDrop();
    }

    void Update()
    {
        UpdatePosition();

        // Check if the loot has finished bouncing and should start following the player
        if (isFollowingPlayer && playerTransform != null)
        {
            // Calculate the direction towards the player
            Vector3 directionToPlayer = (playerTransform.position - t_parent.position).normalized;

            // Move the loot towards the player using a certain speed
            float followSpeed = 5f; // Adjust this value to control the follow speed
            t_parent.position += directionToPlayer * followSpeed * Time.deltaTime;
        }
    }

    void Initialize(Vector2 groundvelocity)
    {
        isGrounded = false;
        // Slow down the height of bounce
        afterVelocity /= settings.YReducer;
        this.groundVelocity = groundvelocity;
        this.verticalVelocity = afterVelocity;
        bounces++;
    }


    // Call this method to simulate bounce effect
    // On Default it's in the Start()
    public void SimulateDrop()
    {
        StartCoroutine(Simulate());
    }

    private IEnumerator Simulate()
    {
        yield return new WaitForSeconds(0f);
        groundVelocity = new Vector2(Random.Range(-settings.horizontalForce, settings.horizontalForce), Random.Range(-settings.horizontalForce, settings.horizontalForce));
        verticalVelocity = Random.Range(settings.velocity - 1, settings.velocity);
        afterVelocity = verticalVelocity;
        Initialize(groundVelocity);

        if (settings.pickUpType == PickUpType.IMMEDIATELY)
            ChangeItemToBeCollectable();

        yield return null;
    }

    private void UpdatePosition()
    {
        if (!isGrounded)
        {
            verticalVelocity += settings.gravity * Time.deltaTime;

            // if item didnt hit anything let it move on X axis
            if (!collide)
            {
                t_parent.position += (Vector3)groundVelocity * Time.deltaTime;
            }
            t_body.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;


            CheckGroundHit();
        }
    }

    /// <summary>
    /// If number of bounces is less than current bounces, it will add force to the item
    /// Force is each bounce reduced by XReducer and YReducer
    /// </summary>
    private void CheckGroundHit()
    {
        if (t_body.position.y < t_shadow.position.y)
        {
            t_body.position = t_shadow.position;

            if (bounces < settings.numberOfBounces)
            {
                Initialize(new Vector2(groundVelocity.x / settings.XReducer, groundVelocity.y / settings.XReducer));
            }
            else
            {

                // Item can be collected
                if (settings.pickUpType == PickUpType.AFTER)
                    ChangeItemToBeCollectable();

                // Give item shadow after last bounce
                if (settings.shadow)
                    t_shadow.position = new Vector3(t_shadow.position.x, t_shadow.position.y - 0.05f, t_shadow.position.z);

                // Prevent item moving
                isGrounded = true;
            }

        }
    }

    private void ChangeItemToBeCollectable()
    {
        pickUpCollision.enabled = true;
        canCollect = true;

        isFollowingPlayer = true;
    }

    private bool CompareCollisionTags(Collider2D collider)
    {
        return settings.colliderTags.Contains(collider.tag);
    }

    // This will Add 2 BoxColliders2D when Loot script has been added to GameObject in Inspector
    private void Reset()
    {
        this.gameObject.AddComponent<BoxCollider2D>();
        this.gameObject.AddComponent<BoxCollider2D>();
    }

    private void toSaveLibrary()
    {
        switch (librarySaveList)
        {
            case LibrarySaveList.Palabok:
                libraryManager.Palabok = true;
                break;
            case LibrarySaveList.Pancit:
                libraryManager.Pancit = true;
                break;
            case LibrarySaveList.Sinigang:
                libraryManager.Sinigang = true;
                break;
            case LibrarySaveList.Adobo:
                libraryManager.Adobo = true;
                break;
            case LibrarySaveList.Lechon:
                libraryManager.Lechon = true;
                break;
            case LibrarySaveList.Tinola:
                libraryManager.Tinola = true;
                break;
            case LibrarySaveList.KareKare:
                libraryManager.KareKare = true;
                break;
            case LibrarySaveList.Lugaw:
                libraryManager.Lugaw = true;
                break;
            case LibrarySaveList.Sopas:
                libraryManager.Sopas = true;
                break;
            case LibrarySaveList.Champorado:
                libraryManager.Champorado = true;
                break;
            case LibrarySaveList.HaloHalo:
                libraryManager.HaloHalo = true;
                break;
            case LibrarySaveList.Balut:
                libraryManager.Balut = true;
                break;
            case LibrarySaveList.Puto:
                libraryManager.Puto = true;
                break;
            case LibrarySaveList.Bibingka:
                libraryManager.Bibingka = true;
                break;
            case LibrarySaveList.Taho:
                libraryManager.Taho = true;
                break;
            case LibrarySaveList.Sisig:
                libraryManager.Sisig = true;
                break;
            case LibrarySaveList.Laing:
                libraryManager.Laing = true;
                break;
            case LibrarySaveList.Longganisa:
                libraryManager.Longganisa = true;
                break;
            case LibrarySaveList.Balisong:
                libraryManager.Balisong = true;
                break;
            case LibrarySaveList.Kris:
                libraryManager.Kris = true;
                break;
            case LibrarySaveList.Bolo:
                libraryManager.Bolo = true;
                break;
            case LibrarySaveList.Barong:
                libraryManager.Barong = true;
                break;
            case LibrarySaveList.Punyal:
                libraryManager.Punyal = true;
                break;
            case LibrarySaveList.Sundang:
                libraryManager.Sundang = true;
                break;
            case LibrarySaveList.Kampilan:
                libraryManager.Kampilan = true;
                break;
            case LibrarySaveList.Arnis:
                libraryManager.Arnis = true;
                break;
            default:
                Debug.Log("Nothing Selected");
                break;
        }
    }


    #region SPRITE RENDER PARTS 

    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndBody;
    private SpriteRenderer sprRndShadow;

    /// <summary>
    /// Will create a icon Sprite Renderer to a Parent
    /// </summary>
    void CreateBody()
    {
        t_parent = transform;
        t_body = new GameObject().transform;
        t_body.parent = t_parent;
        t_body.gameObject.name = "Body";
        t_body.localRotation = Quaternion.identity;
        t_body.localPosition = Vector3.zero;
        sprRndCaster = GetComponent<SpriteRenderer>();
        sprRndBody = t_body.gameObject.AddComponent<SpriteRenderer>();
        sprRndBody.sortingLayerName = sprRndCaster.sortingLayerName;
        sprRndBody.sortingOrder = sprRndCaster.sortingOrder;
        sprRndBody.sprite = sprRndCaster.sprite;
    }

    /// <summary>
    /// Will create a shadow Sprite Renderer to a Parent
    /// </summary>
    void CreateShadow()
    {
        t_parent = transform;
        t_shadow = new GameObject().transform;
        t_shadow.parent = t_parent;
        t_shadow.gameObject.name = "Shadow";
        t_shadow.localRotation = Quaternion.identity;
        t_shadow.localPosition = Vector3.zero;
        sprRndCaster = GetComponent<SpriteRenderer>();
        sprRndShadow = t_shadow.gameObject.AddComponent<SpriteRenderer>();
        sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
        sprRndShadow.color = Color.black;
        sprRndShadow.sprite = sprRndCaster.sprite;
    }

    #endregion
}

// You can also make it as Scriptable Object.
[System.Serializable]
public class LootSettings
{
    [Tooltip("XReducer will slow down horizontal axis ( left right top bottom movement )")]
    [Range(1f, 2.5f)]
    public float YReducer = 1.5f;

    [Tooltip("YReducer will slow down vertical axis ( height of the bounce )")]
    [Range(1f, 2.5f)]
    public float XReducer = 1.5f;

    public int numberOfBounces = 3;

    [Tooltip("Amount of vertical force")]
    public float velocity = 10;

    [Tooltip("Amount of horizontal force")]
    public float horizontalForce = 2;

    public float gravity = -30;

    [Tooltip("Tag of entity who can collect this item")]
    public string collectorTag = "Player";

    [Tooltip("When can Player pick up item")]
    public PickUpType pickUpType = PickUpType.AFTER;

    [Tooltip("It will create small shadow after last bounce")]
    public bool shadow = true;

    public float destroyTime = 0f;

    [Tooltip("When Item hits the wall with that tag")]
    public string[] colliderTags;

}


public enum PickUpType
{
    IMMEDIATELY,
    NEVER,
    AFTER
}

public enum LibrarySaveList
{
    Palabok,
    Pancit,
    Sinigang,
    Adobo,
    Lechon,
    Tinola,
    KareKare,
    Lugaw,
    Sopas,
    Champorado,
    HaloHalo,
    Balut,
    Puto,
    Bibingka,
    Taho,
    Sisig,
    Laing,
    Longganisa,
    Balisong,
    Kris,
    Bolo,
    Barong,
    Punyal,
    Sundang,
    Kampilan,
    Arnis
}