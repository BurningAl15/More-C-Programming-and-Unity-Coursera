using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An asteroid
/// </summary>
public class Asteroid : MonoBehaviour {
    
    public Collider2D col;
    public ScreenWrapper screenLimit;

    [SerializeField]
    Sprite[] asteroids=new Sprite[3];

    Vector3 size;
    float radius;

    private void Awake()
    {
        col=GetComponent<CircleCollider2D>();
        col.GetComponent<CircleCollider2D>().enabled=true;
        screenLimit=GetComponent<ScreenWrapper>();
        screenLimit.GetComponent<ScreenWrapper>().enabled=true;
    }

    public void Initialize(Direction direction,Vector3 position)
    {
        GetComponent<SpriteRenderer>().sprite=asteroids[Random.Range(0,asteroids.Length)];
        this.transform.position=new Vector3(position.x,position.y,0f);
		// apply impulse force to get game object moving
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
        
        size=this.transform.localScale;
        radius=GetComponent<CircleCollider2D>().radius;
        StartMoving(angle);   
    }

    void StartMoving(float tempAngle)
    {
        float MinImpulseForce = 1f;
        float MaxImpulseForce = 1.5f;

        if(size.x>=1)
        {
            MinImpulseForce = 1f;
            MaxImpulseForce = 1.5f;
        }
        else if(size.x<1 && size.x>=0.5f)
        {
            MinImpulseForce = 1.5f;
            MaxImpulseForce = 2f;
        }
        else if(size.x<.5f && size.x>=0.3f)
        {
            MinImpulseForce = 2f;
            MaxImpulseForce = 2.5f;
        }


        Vector2 moveDirection = new Vector2(Mathf.Cos(tempAngle), Mathf.Sin(tempAngle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(moveDirection * magnitude,ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        size=new Vector3(transform.localScale.x/1.5f,transform.localScale.y/1.5f,transform.localScale.z);
        radius=GetComponent<CircleCollider2D>().radius/2;

        if(collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(this.gameObject);
        }
        if(size.x>=0.3f)
        {
            float angle=Random.Range(0f,2f)*Mathf.PI;
            for(int i=0;i<2;i++)
            {
                GameObject asteroidClone=Instantiate(gameObject,transform.position,transform.rotation);
                asteroidClone.tag="Asteroid";
                asteroidClone.transform.localScale=size;
                asteroidClone.GetComponent<CircleCollider2D>().radius=radius;
                asteroidClone.GetComponent<Asteroid>().StartMoving(angle);
                asteroidClone.GetComponent<Asteroid>().col=asteroidClone.GetComponent<CircleCollider2D>();
                angle=Random.Range(0f,2f)*Mathf.PI;
            }
        }
        Destroy(this.gameObject);
        AudioManager.Play(AudioClipName.AsteroidHit);
    }
}
