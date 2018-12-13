using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ship
/// </summary>
public class Ship : MonoBehaviour
{
    // thrust and rotation support
    Rigidbody2D rgb;
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 10;
    const float RotateDegreesPerSecond = 180;

    [SerializeField]
    GameObject Hud;

    // screen wrapping support
    float colliderRadius;

    [SerializeField]
    GameObject prefabBullet;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
		// saved for efficiency
        rgb = GetComponent<Rigidbody2D>();
        colliderRadius = GetComponent<CircleCollider2D>().radius;
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        // check for rotation input
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0) {

            // calculate rotation amount and apply rotation
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // change thrust direction to match ship rotation
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject bullet= Instantiate(prefabBullet,transform.position,transform.rotation);
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
            AudioManager.Play(AudioClipName.PlayerShot);
        }
	}

    /// <summary>
    /// FixedUpdate is called 50 times per second
    /// </summary>
    void FixedUpdate()
    {
        // thrust as appropriate
        if (Input.GetAxis("Thrust") != 0)
        {
            rgb.AddForce(ThrustForce * thrustDirection,
                ForceMode2D.Force);
        }
    }

    /// <summary>
    /// Called when the game object becomes invisible to the camera
    /// </summary>
    void OnBecameInvisible()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hud.GetComponent<HUDManager>().StopGameTimer();
        AudioManager.Play(AudioClipName.PlayerDeath);
        
        Destroy(this.gameObject);
    }
}
