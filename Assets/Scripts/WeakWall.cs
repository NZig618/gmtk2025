using UnityEngine;

public class WeakWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Die when shot
        if (collision.gameObject.CompareTag("Bullet1") || collision.gameObject.CompareTag("Bullet2"))
        {
            Destroy(gameObject);
        }
    }
}
