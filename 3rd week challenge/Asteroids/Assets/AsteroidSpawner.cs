using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asteroid Spawner
/// </summary>
public class AsteroidSpawner : MonoBehaviour {

    [SerializeField]
    GameObject prefabAsteroid;

    float radius;

    void AsteroidInitialize()
    {
        GameObject asteroid = Instantiate(prefabAsteroid);
        asteroid.GetComponent<Asteroid>().Initialize(Direction.Left, this.transform.position);
        radius = asteroid.GetComponent<CircleCollider2D>().radius;
        Destroy(asteroid);
    }

    void Start () {
        AsteroidInitialize();
        GameObject left= Instantiate(prefabAsteroid);
        left.GetComponent<Asteroid>().Initialize(Direction.Left , new Vector2(ScreenUtils.ScreenRight-radius,(ScreenUtils.ScreenTop+ScreenUtils.ScreenBottom)/2));
        GameObject right= Instantiate(prefabAsteroid);
        right.GetComponent<Asteroid>().Initialize(Direction.Right , new Vector2(ScreenUtils.ScreenLeft+radius,(ScreenUtils.ScreenTop+ScreenUtils.ScreenBottom)/2));
        GameObject bottom= Instantiate(prefabAsteroid);
        bottom.GetComponent<Asteroid>().Initialize(Direction.Down , new Vector2((ScreenUtils.ScreenRight+ScreenUtils.ScreenLeft)/2,ScreenUtils.ScreenTop-radius));
        GameObject top= Instantiate(prefabAsteroid);
        top.GetComponent<Asteroid>().Initialize(Direction.Up , new Vector2((ScreenUtils.ScreenRight+ScreenUtils.ScreenLeft)/2,ScreenUtils.ScreenBottom+radius));

	}
}
