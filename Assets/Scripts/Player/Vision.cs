using UnityEngine;
using System.Collections.Generic;

public class Vision : MonoBehaviour
{
    public float viewDist = 10;
    public LayerMask lm;

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10, lm);
        List<Vector2> points = new List<Vector2>();
        points.Add(transform.position);  // Add origin
        // Add left and right edge of view

        foreach(Collider2D col in colliders)
        {
            if(col is PolygonCollider2D)
            {
                Vector2[] p = ((PolygonCollider2D)col).points;
                foreach(Vector2 v in p)
                    points.Add(v);
            }else if(col is BoxCollider2D)
            {
                Vector2 size = ((BoxCollider2D)col).size;
                Vector2 pos = col.transform.position;
                points.Add(pos + size / 2); // Top Right
                points.Add(pos - size / 2); // Bottom Left

                size.Scale(Vector2.down);
                points.Add(pos + size / 2); // Bottom Right
                points.Add(pos - size / 2); // Top left
            }
            // Other Colliders
        }

        // Order points clockwise (ignoring origin in position 0)

        int numTris = points.Count * 3;
        int[] tris = new int[numTris];
        // Create tris
        for(int i = 0; i <= points.Count; i++)
        {
            tris[i * 3]     = 0;
            tris[i * 3 + 1] = i;
            tris[i * 3 + 2] = i + 1;
        }


        Mesh mesh = new Mesh();
    }
}
