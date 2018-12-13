using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    Rigidbody2D rgb;
    const float magnitude=10;
    const float deathTimer=1f;
    Timer timer;

    float colliderRadius;
    private void Awake()
    {
        rgb=GetComponent<Rigidbody2D>();
        colliderRadius=GetComponent<CircleCollider2D>().radius;
        timer=gameObject.AddComponent<Timer>();
        timer.Duration=deathTimer;
        timer.Run();
    }

    /// <summary>
    /// Applies force to the bullet
    /// </summary>
    /// <param name="direction"></param>
    public void ApplyForce(Vector2 direction)
    {
        rgb.AddForce(magnitude * direction,ForceMode2D.Impulse);
    }

    private void Update()
    {
        if(timer.Finished)
            Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Vector2 position = transform.position;

        // check left, right, top, and bottom sides
        if (position.x + colliderRadius < ScreenUtils.ScreenLeft ||
            position.x - colliderRadius > ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }
        if (position.y - colliderRadius > ScreenUtils.ScreenTop ||
            position.y + colliderRadius < ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
        }

        // move ship
        transform.position = position;

    }
}
