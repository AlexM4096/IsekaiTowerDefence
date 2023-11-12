using System.Collections.Generic;
using Tools.DelaunayTriangulation;
using UnityEngine;
using Edge = Tools.DelaunayTriangulation.Edge;

public class Test : MonoBehaviour
{
    public void ToTest()
    {
        List<Point> points = new List<Point>()
        {
            new Point(new Vector2(0, 0)), 
            new Point(new Vector2(0, 10)), 
            new Point(new Vector2(10, 0)),
        };

        ICollection<Edge> edges = Triangulator.Triangulate(points);

        print(edges.Count);
    }
}