using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static float speed;
    public static float minX;
    public static float maxX;
    public static float minY;
    public static float maxY;

    private Camera camera;
    private List<GameObject> asteroids;

    [SerializeField]
    GameObject wallPrefab;
    [SerializeField]
    Material material;
    [SerializeField]
    GameObject asteroidPrefab;

    void Start()
    {
        camera = Camera.main;
        asteroids = new List<GameObject>();

        minX = camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, 0)).x;
        minY = camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, 0)).y;

        GameObject leftWall = GameObject.Instantiate(wallPrefab);
        leftWall.transform.localScale = new Vector3(1, maxY - minY, 0);
        leftWall.transform.position = new Vector3(minX - 0.5f, (minY + maxY) / 2f, 0);

        GameObject rightWall = GameObject.Instantiate(wallPrefab);
        rightWall.transform.localScale = new Vector3(1, maxY - minY, 0);
        rightWall.transform.position = new Vector3(maxX + 0.5f, (minY + maxY) / 2f, 0);

        GameObject topWall = GameObject.Instantiate(wallPrefab);
        topWall.transform.localScale = new Vector3(maxX - minX, 1, 0);
        topWall.transform.position = new Vector3((minX + maxX) / 2f, maxY+0.5f, 0);

        GameObject bottomWall = GameObject.Instantiate(wallPrefab);
        bottomWall.transform.localScale = new Vector3(maxX - minX, 1, 0);
        bottomWall.transform.position = new Vector3((minX + maxX) / 2f, minY-0.5f, 0);

        for (int i = 0; i < 5; i++)
        {
            GameObject gobj = generateNewAsteroid();
            gobj.transform.position = new Vector3(Random.Range(minX+1, maxX-1), Random.Range(minY+1, maxY-1), 0);
        }
        

    }

    public GameObject generateNewAsteroid()
    {
        GameObject asteroid = GameObject.Instantiate(asteroidPrefab);
        return asteroid;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        if(asteroids.Length < 5)
        {
            GameObject gobj = generateNewAsteroid();
            gobj.transform.position = new Vector3(Random.Range(minX + 1, maxX - 1), Random.Range(minY + 1, maxY - 1), 0);
        }
    }
}
