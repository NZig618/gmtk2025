using UnityEngine;

public class StrongWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Die when shot
        if (collision.gameObject.CompareTag("Bullet2"))
        {
            Destroy(gameObject);
        }
    }
}
