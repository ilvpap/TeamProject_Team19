using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Item
{
    public override void Activate(PlayerStats player)
    {
        /*player.StartCoroutine(BoostRoutine(player)); */  //코루틴 시작
    }

    //private IEnumerator BoostRoutine(PlayerController player)
    //{
    //    float originalSpeed = player.moveSpeed;                 //원래속도
    //    player.moveSpeed += 5f;                                 //이동속도 +5
    //    yield return new WaitForSeconds(value);                 // value만큼 지속시간
    //    player.moveSpeed = originalSpeed;                       // 원래속도로 
    //}
}
