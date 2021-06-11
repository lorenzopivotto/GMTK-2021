using UnityEngine;

public class Asteroid : MonoBehaviour
{

    private Rigidbody2D componentRigidBody;
    public float speed;

    private void Start()
    {
        componentRigidBody = GetComponent<Rigidbody2D>();

        Vector3 directionToPlayer = (GameManager.Player.position - transform.position).normalized;
        componentRigidBody.AddForce(directionToPlayer * speed, ForceMode2D.Impulse);
    }

}
