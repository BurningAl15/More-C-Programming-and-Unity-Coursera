using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour {
    
    [SerializeField]
    Sprite[] asteroids=new Sprite[3];

    public void Initialize(Direction direction,Vector3 position)
    {
        GetComponent<SpriteRenderer>().sprite=asteroids[Random.Range(0,asteroids.Length)];
        this.transform.position=new Vector3(position.x,position.y,0f);
		// apply impulse force to get game object moving
        const float MinImpulseForce = 2f;
        const float MaxImpulseForce = 3.5f;

        float angle=0;

        switch(direction)
        {
            case Direction.Up:
                angle = Random.Range(75*Mathf.Deg2Rad,105*Mathf.Deg2Rad);
                break;
            case Direction.Down:
                angle = Random.Range(255*Mathf.Deg2Rad,285*Mathf.Deg2Rad);
                break;
            case Direction.Left:
                angle = Random.Range(165*Mathf.Deg2Rad,195*Mathf.Deg2Rad);
                break;
            case Direction.Right:
                angle = Random.Range(-15*Mathf.Deg2Rad,15*Mathf.Deg2Rad);
                break;
        }
        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
            Destroy(collision.gameObject);

        if(collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
