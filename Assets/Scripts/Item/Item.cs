using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Player player;
    public int value = 0;

    public virtual void ApplyEffect(Player player) { }
    public virtual void Activate(Player player) { }

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
