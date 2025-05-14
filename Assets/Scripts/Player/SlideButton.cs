using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlideButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController playerController;

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.Slide(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerController.Slide(false);
    }

}
