using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    public SpriteRenderer mapSpriteRenderer;
    Camera thisCamera;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        playerTransform = Player.Instance.transform;
        thisCamera = GetComponent<Camera>();

        screenBounds = thisCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, thisCamera.transform.position.z));
        objectWidth = mapSpriteRenderer.bounds.extents.x; //extents = size of width / 2
        objectHeight = mapSpriteRenderer.bounds.extents.y; //extents = size of height / 2
    }
    void LateUpdate()
    {
        this.transform.position = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y,
            this.transform.position.z
            );

        Vector3 viewPos = this.transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x - objectWidth, screenBounds.x * -1 + objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y - objectHeight, screenBounds.y * -1 + objectHeight);
        this.transform.position = viewPos;

    }
}
