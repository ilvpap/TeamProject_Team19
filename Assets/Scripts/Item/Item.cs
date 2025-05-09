using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public PlayerController player;
    public int value = 0;

    public virtual void ApplyEffect(PlayerController player) { }
    public virtual void Activate(PlayerController player) { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            ApplyEffect(player);
            Activate(player);
            Destroy(gameObject);

        }
    }
}
