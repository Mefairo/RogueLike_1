using UnityEngine;

public class TestPush : MonoBehaviour
{
    public float force = 5f; // Сила отталкивания

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("111");
        if (other.CompareTag("Test"))
        {
            Debug.Log("112");
            Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {
                Debug.Log("113");
                Vector3 direction = (other.transform.position - transform.position).normalized;
                rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }
}