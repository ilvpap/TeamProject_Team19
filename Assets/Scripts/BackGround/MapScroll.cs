using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll : MonoBehaviour
{
    public float scrollSpeed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
    }
}
