using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float limitLeft = -6;
    public float limitRight = 1000;

    public float moveSpeed = 2;
    public float distanceScale = 1;
    float beginX;
    float beginCamreaPosX;
    bool isDragging = false;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        beginCamreaPosX = transform.position.x;
        target = transform.position;
        target.x = Mathf.Clamp(transform.position.x, limitLeft + CameraHalfWidth, limitRight - CameraHalfWidth);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(beginCamreaPosX + (Input.mousePosition.x - beginX) * moveSpeed * 0.01f, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.fixedDeltaTime);

        if (!isDragging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                beginX = Input.mousePosition.x;
                beginCamreaPosX = transform.position.x;
            }
        }
        else
        {
            if(Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
            else
            {
                target = new Vector3(beginCamreaPosX + (beginX - Input.mousePosition.x) * distanceScale * 0.01f, transform.position.y, transform.position.z);

                target.x = Mathf.Clamp(target.x, limitLeft + CameraHalfWidth, limitRight - CameraHalfWidth);
            }
        }
    }

    public float CameraHalfWidth
    {
        get { return (Camera.main.orthographicSize * ((float)Screen.width / Screen.height)); }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.color = Color.yellow;
        Vector2 boxSize = new Vector2(limitRight - limitLeft, Camera.main.orthographicSize * 2);
        Vector2 center = new Vector2((limitRight + limitLeft) * 0.5f, transform.position.y);
        Gizmos.DrawWireCube(center, boxSize);

    }
}
