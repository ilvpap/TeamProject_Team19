using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Player player;
    public int value = 10; //나중에 변경

    public virtual void ApplyEffect(PlayerStats player) { }
    public virtual void Activate(Player player) { }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Player player = collision.gameObject.GetComponent<Player>(); 
        if (player != null)
        {
            
            PlayerStats stat = player.Stat;
            ApplyEffect(stat);
            Activate(player);
            Destroy(gameObject);

        }
    }
   
}
