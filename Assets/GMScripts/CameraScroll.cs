using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        Vector2 VerticalScroll = Vector2.up * -scrollSpeed * Time.deltaTime;
        transform.Translate(0, VerticalScroll.y, 0);
    }
}
