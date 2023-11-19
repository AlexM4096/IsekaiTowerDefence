using System.Collections.Generic;
using Tools;
using Tools.DelaunayTriangulation;
using UnityEngine;

public class Test : MonoBehaviour
{
    private ICollection<Triangle> triangles;
    
    public void ToTest()
    {
        List<Point> points = new List<Point>()
        {
            new Point(new Vector2(0, 0)), 
            new Point(new Vector2(0, 10)), 
            new Point(new Vector2(10, 0)),
            new Point(new Vector2(10, 10)),
        };

        triangles = Triangulator.Triangulate(points);

        print(triangles.Count);
    }

    private void OnDrawGizmos()
    {
        if (triangles == null) return;

        foreach (var triangle in triangles)
        {
            Point ver1 = triangle.Vertices[0];
            Point ver2 = triangle.Vertices[1];
            Point ver3 = triangle.Vertices[2];
            
            Gizmos.DrawLine(ver1.Value.ToVector3(), ver2.Value.ToVector3());
            Gizmos.DrawLine(ver2.Value.ToVector3(), ver3.Value.ToVector3());
            Gizmos.DrawLine(ver3.Value.ToVector3(), ver1.Value.ToVector3());
        }
    }
}