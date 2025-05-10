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
