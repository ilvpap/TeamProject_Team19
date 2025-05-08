using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] private bool rotate = false;
    [SerializeField] private float rotateSpeed = 100f;

    private void Update()
    {
        if(rotate)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.TakeDamage();
        }
    }
}
