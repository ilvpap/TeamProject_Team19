using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerStats
{
    public float curHp; //필드 선언
    public float maxHp;

    public float MaxHp { get { return maxHp; } } //get set 프로퍼티 get은 (읽기를 외부에서 가능하게) set은 (쓰기를 외부에서 가능하게)
    public float CurHp { get { return curHp; } }
    public float HpRatio => curHp / maxHp; //현재 HP %  -> 0 ~ 1 (보통 비율 계산은 0에서 1까지)
    //public float CurHp => curHp; 위 코드와 동일
    //public float CurHp {get; private set;} 위 코드와 동일 (필드 필요 없음. 내부에서 필드 구현됨.)
    public PlayerStats(float hp)
    {
        curHp = hp;
        maxHp = hp;
    }
    public void GetDamage(float damage)
    {
        curHp -= damage;
        curHp = Mathf.Clamp(curHp, 0, MaxHp);
        if(curHp <= 0)
        {
            //Todo: 게임매니저에서 게임오버를 만들었을 경우 해당 함수를 추후 사용.
            GameOver();
        }
    }
    public void GameOver()
    {

    }
}
