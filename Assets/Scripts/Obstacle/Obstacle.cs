using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public float widthPaddingMin = 3f;
    public float widthPaddingMax = 6f;

    public Transform topObject;
    public Transform bottomObject;
    public Transform top2Object;

    public string Obstaclename;
    public float downspeed = -1f;
    public GameObject Topobj;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float Dis;
    [SerializeField]
    private bool IsDown = false;
    [SerializeField]
    private float YRange;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {

        if (IsDown)
        {
            if (Topobj.transform.position.y >= YRange)
            {
                Topobj.transform.Translate(new Vector3(0, downspeed * Time.deltaTime, 0));
            }           
        }
        else
        {
            if (Dis <= 16)
            {

                IsDown = true;

            }
        }
        Dis = Vector3.Distance(Topobj.transform.position, player.transform.position);
        
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount) 
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;

        topObject.localPosition = new Vector3( 0 , halfHoleSize);
        bottomObject.localPosition = new Vector3( 0, halfHoleSize);
        top2Object.localPosition = new Vector3( 0, halfHoleSize);
        
        float randomPadding = Random.Range(widthPaddingMin, widthPaddingMax);
        Vector3 placePosition = lastPosition + new Vector3(randomPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }
}
