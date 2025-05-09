using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : Item
{
    [SerializeField] private ScoreManager scoreManager;
    public override void ApplyEffect(PlayerController player)
    {
        scoreManager.AddScore(value); // Item.cs에서 Value 값 가져옴
    }
}
