using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private static LightController instance = null;
    public static LightController Instance { get{return instance;} }

    public float walkSpeed = 2f;
    public float inertiaTime = 0.2f;
    private float currentInertiaTime = 0f;
    public float minimumSize = 0.25f;
    public float maximumSize = 0.75f;

    public float sizeMultiplier = 1f;

    private Rigidbody2D rigidbody2d;

    private Vector2 moveDir;
    private LightSettings lightSettings;

    private Vector2 zeroVector = Vector2.zero;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }
    void Start()
    {
        moveDir = Vector2.zero;
        rigidbody2d = GetComponent<Rigidbody2D>();

        lightSettings = GetComponentInChildren<LightSettings>();
        lightSettings.setSizeInstant(maximumSize);
    }

    void Update()
    {
        Vector2 move = Vector2.zero;
        //INPUTS
        if (Input.GetKey(KeyCode.W)) move.y = 1;
        if (Input.GetKey(KeyCode.S)) move.y = -1;
        if (Input.GetKey(KeyCode.A)) move.x = -1;
        if (Input.GetKey(KeyCode.D)) move.x = 1;

        if (move.Equals(zeroVector))
        {
            this.currentInertiaTime += Time.deltaTime;
            if (this.currentInertiaTime < this.inertiaTime)
                return;
        }
        else
            this.currentInertiaTime = 0f;
        

        moveDir = move.normalized;
    }
    private void FixedUpdate()
    {
        float inertiaInfluence = 1 - Mathf.Lerp(0, 1, this.currentInertiaTime / this.inertiaTime);
        rigidbody2d.MovePosition(Vector2.MoveTowards(rigidbody2d.position, rigidbody2d.position + moveDir, walkSpeed * inertiaInfluence * Time.fixedDeltaTime));
    }

    public void setLightSize(float health, float maxHealth)
    {
        float size = Mathf.Lerp(this.minimumSize, this.maximumSize, health / maxHealth);
        lightSettings.setSize(size);
    }
    public void setLightSizeMultiplier(float multiplier)
    {
        sizeMultiplier = multiplier;
        lightSettings.setSize(lightSettings.transform.localScale.x * sizeMultiplier);
    }

    public void EnableLight()
    {
        lightSettings.gameObject.SetActive(true);
    }
    public void DisableLight()
    {
        lightSettings.gameObject.SetActive(false);
    }
}
