using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Player player;
    public int value = 100; //나중에 변경

    public virtual void ApplyEffect(PlayerStats player) { }
    public virtual void Activate(PlayerStats player) { }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Player playerComponent = collision.gameObject.GetComponent<Player>(); 
        if (playerComponent != null)
        {
            
            PlayerStats stat = playerComponent.Stat;
            ApplyEffect(stat);
            Activate(stat);
            Destroy(gameObject);

        }
    }
   
}
