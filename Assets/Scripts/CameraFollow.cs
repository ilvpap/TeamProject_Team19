// Assets\Scripts\CameraFollow.cs

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target;   // 플레이어 Transform
    [SerializeField] private Vector3 offset;     // 플레이어와의 거리/위치 오프셋
    [SerializeField] private float smoothSpeed = 0.125f; // 부드럽게 따라가는 정도 (0~1 사이가 적당)

    private void LateUpdate()
    {
        if (target == null) return;

        // 원하는 최종 위치
        Vector3 desiredPosition = target.position + offset;
        // 카메라 위치를 부드럽게 보간
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
