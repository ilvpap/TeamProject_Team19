using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapScroll : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float boostSpeed = 20f;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        float normal = scrollSpeed;
        if(playerController != null && playerController.isBoosted)
        {
            normal = boostSpeed;
        }

        transform.Translate(Vector3.left * normal * Time.deltaTime);
    }
}
