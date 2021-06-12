using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public static int index;

    public float size;
    public int localIndex;
    // Start is called before the first frame update
    void Start()
    {
        localIndex = index;
        index++;
        size = 1;
        buildSelf(size, 12);
    }

    void buildSelf(float radius, int vertexCount)
    {
        GameObject gobj = this.gameObject;
        PolygonCollider2D collider = this.GetComponent<PolygonCollider2D>();
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        //PolygonCollider2D collider = gobj.AddComponent<PolygonCollider2D>();
        //MeshFilter meshFilter = gobj.AddComponent<MeshFilter>();
        //MeshRenderer meshRenderer = gobj.AddComponent<MeshRenderer>();
        //meshRenderer.material = material;

        Mesh mesh = new Mesh();
        mesh.Clear();


        Vector3[] verticies = new Vector3[vertexCount + 1];
        verticies[0] = new Vector3(0, 0, 0);
        for (int i = 1; i < verticies.Length; i++)
        {
            float r = Random.Range(0.5f, radius);
            float radians = Mathf.PI * 2 * (1.0f * (i - 1) / (vertexCount));
            //Debug.Log(radians);
            float x = r * Mathf.Cos(radians);
            float y = r * Mathf.Sin(radians);
            verticies[i] = new Vector3(x, y, 0);
            //Debug.Log("Vertex " + i + ": " + verticies[i]);
        }
        mesh.vertices = verticies;

        int[] triangles = new int[vertexCount * 3];
        for (int i = 1; i < vertexCount + 1; i++)
        {

            triangles[((i - 1) * 3)] = 0;
            triangles[((i - 1) * 3) + 2] = i;
            if (i + 1 > vertexCount)
            {
                triangles[((i - 1) * 3) + 1] = 1;
            }
            else
            {
                triangles[((i - 1) * 3) + 1] = i + 1;
            }
        }
        mesh.triangles = triangles;

        Vector2[] pointsForCollider = new Vector2[vertexCount];
        for (int i = vertexCount - 1; i > 0; i--)
        {
            pointsForCollider[i] = new Vector2(verticies[i + 1].x, verticies[i + 1].y);
        }
        collider.points = pointsForCollider;

        meshFilter.mesh = mesh;
    }

    private void Awake()
    {
        Vector2 initialDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * 2;
        this.GetComponent<Rigidbody2D>().velocity = initialDir;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        Asteroid otherAsteroid = null;
        Debug.Log(collision.gameObject.GetComponent<Asteroid>());
        
        if((otherAsteroid = collision.gameObject.GetComponent<Asteroid>()) != null)
        {
            if(this.localIndex > otherAsteroid.localIndex)
            {
                this.size += 0.25f * otherAsteroid.size;
                Destroy(collision.gameObject);
                this.buildSelf(this.size, 12);
            }
            

        }
    }
}
