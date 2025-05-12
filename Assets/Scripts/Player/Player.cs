using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStats stat;
    public PlayerStats Stat => stat; // 프로퍼티 프로퍼티의 식 본문 (식으로 구성된 프로퍼티의 본문을 의미한다)
    private void Awake()
    {
        stat = new PlayerStats(100);
    }
    private void Start()
    {
        StartCoroutine(GetDamagePerTime()); //함수
    }
    //private void Update()
    //{
    //    stat.GetDamage(0.01f);
    //} > 데미지가 더 다는 현상이 일어났는데 private void Update .. 때문이엇다
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<PlayerController>().isShielded)
        {
            return;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            //방해물의 데미지를 바꿀 경우
            //stat.GetDamage = collision.GetComponent<Obstacle>().damage
            stat.GetDamage(10);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("GameOverLine"))
        {
            stat.GetDamage(stat.MaxHp);
        }

        Debug.Log("<color=red>"+ stat.CurHp + "</color>");

    }

    IEnumerator GetDamagePerTime() // 동기: 한개 한개 실행함 (직렬),  비동기: 병행함 (병렬), 코루틴 : 비동기처럼 보임 (두개의 파일을 순서대로)
    {
        while (true)
        {
            stat.GetDamage(0.01f);
            Debug.Log("내 현재 체력 : <color=blue>" + stat.CurHp + "</color>");
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
