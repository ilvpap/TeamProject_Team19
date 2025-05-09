using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    public override void Activate(Player player)
    {
        player.Shield(value); // Item.cs에서 Value 값 가져옴
    }
}
