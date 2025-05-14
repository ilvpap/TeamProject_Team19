using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //장애물의 최대/최소 Y위치
    public float highPosY = 1f;          //최소
    public float lowPosY = -1f;          //최대
    // 구멍 크기의 최소/최대값 (장애물 사이를 통과하는 공간)
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;
    // 장애물 간 가로 간격의 최소/최대값
    public float widthPaddingMin = 3f;   //최소 값
    public float widthPaddingMax = 6f;   //최대 값
    // 장애물을 구성하는 상단, 하단, 상단 추가 오브젝트
    public Transform topObject;          //상단   마찬가지로 여기도 탑은 안쓰고 바텀만 쓰고싶다 하시면 유티니에서 빼면됩니다.
    public Transform bottomObject;       //하단   참고로 바텀은 안쓰고 탑만 쓰고싶다 하시면 유니티에서 이 오브젝트를 빼면 됩니다. 무조건 유니티에서 빼야합니다.
    public Transform top2Object;         //상단 추가 오브젝트

    public string Obstaclename;          // 장애물 이름 (식별용)   이름은 아직 안정했습니다. 유니티에서 정하시면 됩니다.  근데 이거는 사실상 필요없습니다.
    
    public float downspeed = -1f;        // 위에서 아래로 이동하는 속도      이 값도 그냥 유니티에서 정하시면 됩니다.
    
    private bool IsUP = false;           // 하단 장애물이 위로 움직이는지 여부
    
    
    [SerializeField]
    private float Hight;                 // 하단 장애물이 도달해야 하는 Y 위치    여기서 하단 장애물이 어디서 멈출지 정해줄수 있습니다.                                        
    
    
    [SerializeField]
    private float BottomDis;             // 하단 장애물과 플레이어 사이의 거리
    
    
    public GameObject Topobj;            // 상단 장애물 오브젝트


    [SerializeField]
    private GameObject player;           // 플레이어 오브젝트 참조


    [SerializeField]
    private float Dis;                   // 상단 장애물과 플레이어 사이 거리


    [SerializeField]
    private bool IsDown = false;         // 상단 장애물이 아래로 내려가는지 여부


    [SerializeField]
    private float YRange;                // 상단 장애물이 내려갈 Y 위치 범위

    private void Start()
    {
        if (player == null)
        {
            // 플레이어 오브젝트가 할당되지 않았다면 태그로 자동 할당
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // 하단 장애물 움직임 처리
    private void BottomObsacle() 
    {
        if (IsUP == true)
        {
            // 하단 장애물이 목표 높이(Hight)까지 도달하지 않았다면 위로 이동
            if (bottomObject.transform.position.y <= Hight)
            {
                bottomObject.transform.Translate(new Vector3(0 ,Random.Range(1,3)* Time.deltaTime, 0));
                //1과 3 사이의 랜덤한 정수 값을 생성한 후, 프레임 간 시간 간격(Time.deltaTime)과 곱하여 결과를 반환

            }
        }
        else
        {
            
            if (BottomDis <= 13)      // 플레이어와의 거리가 13 이하라면 상승 시작
            {
                IsUP = true;          //이게 false 가 되면 작동안합니다. 플레이어와 거리가 13이상으로 올라가버리면 자동으로 false 로 바뀝니다.
            }
        }
        if (bottomObject != null)
        {   
            BottomDis = Vector3.Distance(bottomObject.transform.position, player.transform.position);    // 하단 장애물과 플레이어 사이 거리 측정
        }

    }

    private void Update()
    {
        // 매 프레임마다 상단/하단 장애물 위치 처리
        TopObsacle();          //상단 위치 처리 
        BottomObsacle();       //하단 위치 처리
    }

    private void TopObsacle()  // 상단 장애물 움직임 처리
    {
        if (IsDown)   // 상단 장애물이 목표 YRange보다 위에 있다면 아래로 이동
        {
            if (Topobj.transform.position.y >= YRange)
            {
                Topobj.transform.Translate(new Vector3(0, downspeed * Time.deltaTime, 0));
            }
        }
        else
        {
            if (Dis <= 16)             // 플레이어와의 거리가 16 이하라면 하강 시작
            {

                IsDown = true;

            }
        }
        if (Topobj != null)           // 상단 장애물과 플레이어 사이 거리 측정
        {
            Dis = Vector3.Distance(Topobj.transform.position, player.transform.position);
        }
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount) // 장애물의 초기 위치를 무작위로 설정하고, 다음 장애물 위치를 반환
    {
        // 구멍 크기를 랜덤으로 설정
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;

        // 상단/하단/상단추가 장애물 위치 설정 (구멍 중심 기준)
        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, halfHoleSize);
        top2Object.localPosition = new Vector3(0, halfHoleSize);

        // 장애물 간 가로 간격을 랜덤으로 설정
        float randomPadding = Random.Range(widthPaddingMin, widthPaddingMax);
        Vector3 placePosition = lastPosition + new Vector3(randomPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);    // 전체 장애물의 Y 위치도 랜덤하게 지정

        transform.position = placePosition;     // 장애물 위치 설정

        return placePosition;    // 다음 장애물 생성 위치로 반환
    }
}
