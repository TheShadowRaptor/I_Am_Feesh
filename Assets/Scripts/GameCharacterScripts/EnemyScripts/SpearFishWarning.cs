using UnityEngine;

public class SpearFishWarning : MonoBehaviour
{
    GameObject cameraObj;
    Camera camera;

    GameObject playerObj;

    Renderer renderer;
    Color spriteRenderer;

    public bool rightSpawn = true;

    public GameObject spearFish;
    public GameObject spearFishLeft;

    public float startDisappearTime;
    public float disappearTime;
    
    public float startReappearTime;
    public float reappearTime;

    public float cycleTime = 0;
    public float lastCycle = 3;

    public bool startInvisable = false;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>().color;

        if (startInvisable == false)
        {
            disappearTime = startDisappearTime;
            reappearTime = 0;
        }

        else if (startInvisable)
        {
            reappearTime = startReappearTime;
            disappearTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
        }

        if (cameraObj == null)
        {
            cameraObj = GameObject.Find("MainCamera");
            camera = cameraObj.GetComponent<Camera>();
        }

        if (cycleTime == lastCycle)
        {
            if (rightSpawn)
            {
                Instantiate(spearFish, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            else
            {
                Instantiate(spearFishLeft, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if (renderer.isVisible)
        {
            WarningFlash();
            AlignWithPlayer(rightSpawn);
        }

    }

    void WarningFlash()
    {
        if (reappearTime == 0)
        {
            spriteRenderer.a = 0.00f;
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>().color = spriteRenderer;
            disappearTime -= Time.deltaTime;

            if (disappearTime <= 0)
            {
                disappearTime = 0;
                reappearTime = startReappearTime;
                cycleTime += 1;
            }
        }

        if (disappearTime == 0)
        {
            spriteRenderer.a = 1f;
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>().color = spriteRenderer;
            reappearTime -= Time.deltaTime;

            if (reappearTime <= 0)
            {
                reappearTime = 0;
                disappearTime = startDisappearTime;
            }
        }
    }

    void AlignWithPlayer(bool rightSpawn)
    {
        Vector3 pos = transform.position;
        Vector3 camPos = camera.transform.position;
        Vector3 playerPos = playerObj.transform.position;

        pos.y = playerPos.y;

        Vector3 camBounds = new Vector3(camera.orthographicSize * 2, camera.orthographicSize, 0);

        if (rightSpawn)
        {
            pos.x = -camBounds.x + camPos.x + 2;
        }
        else
        {
            pos.x = camBounds.x + camPos.x - 2;
        }

        transform.position = pos;
    }
}
