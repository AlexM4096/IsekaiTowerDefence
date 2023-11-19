using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools.DelaunayTriangulation
{
    public static class Triangulator
    {
        private const float Margin = 3f;
        
        public static Triangle GenerateSupraTriangle(Rect bounds)
        {
            float dMax = Mathf.Max(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin) * Margin;
            float xCen = (bounds.xMin + bounds.xMax) * 0.5f;
            float yCen = (bounds.yMin + bounds.yMax) * 0.5f;
            
            float x1 = xCen - 0.866f * dMax;
            float x2 = xCen + 0.866f * dMax;
            float x3 = xCen;

            float y1 = yCen - 0.5f * dMax;
            float y2 = yCen - 0.5f * dMax;
            float y3 = yCen + dMax;

            Point pointA = new Point(new Vector2(x1, y1));
            Point pointB = new Point(new Vector2(x2, y2));
            Point pointC = new Point(new Vector2(x3, y3));

            return new Triangle(pointA, pointB, pointC);
        }
        
        public static Rect GetPointBounds(IEnumerable<Point> points)
        {
            float xMin = Mathf.Infinity;
            float yMin = Mathf.Infinity;
            float xMax = Mathf.NegativeInfinity;
            float yMax = Mathf.NegativeInfinity;

            foreach (Point point in points)
            {
                if (xMin > point.x)
                    xMin = point.x;

                if (yMin > point.y)
                    yMin = point.y;
                
                if (xMax < point.x)
                    xMax = point.x;

                if (yMax < point.y)
                    yMax = point.y;
            }

            return Rect.MinMaxRect(xMin, yMin, xMax, yMax);
        }
        
        public static ICollection<Triangle> Triangulate(List<Point> points)
        {
            List<Triangle> triangles = new List<Triangle>();
            
            Rect bounds = GetPointBounds(points);
            Triangle supraTriangle = GenerateSupraTriangle(bounds);
            triangles.Add(supraTriangle);
            
            foreach (Point point in points)
            {
                List<Triangle> badTriangles = new List<Triangle>();
        
                //Identify 'bad triangles'
                foreach (Triangle triangle in triangles)
                {
                    //A 'bad triangle' is defined as a triangle who's CircumCentre contains the current point
                    float dist = Vector2.Distance(point.Value, triangle.CircumCentre);
                    
                    if (dist < triangle.CircumRadius)
                    {
                        badTriangles.Add(triangle);
                    }
                }
        
                //Contruct a polygon from unique edges, i.e. ignoring duplicate edges inclusively
                HashSet<Edge> polygon = new HashSet<Edge>();
                for (int i = 0; i < badTriangles.Count; i++)
                {
                    Triangle triangle = badTriangles[i];
                    Edge[] edges = triangle.Edges;
        
                    foreach (var t1 in edges)
                    {
                        bool rejectEdge = false;
                        for (int t = 0; t < badTriangles.Count; t++)
                        {
                            if (t != i && badTriangles[t].ContainsEdge(t1))
                            {
                                rejectEdge = true;
                            }
                        }
        
                        if (!rejectEdge)
                        {
                            polygon.Add(t1);
                        }
                    }
                }
                
                foreach (Triangle triangle in badTriangles)
                {
                    triangles.Remove(triangle);
                }
                
                foreach (var edge in polygon)
                {
                    Point pointA = point;
                    Point pointB = edge.Start;
                    Point pointC = edge.End;
                    
                    triangles.Add(new Triangle(pointA, pointB, pointC));
                }
            }

            for (int i = triangles.Count - 1; i >= 0; i--)
            {
                Triangle triangle = triangles[i];
                for (int j = 0; j < triangle.Vertices.Length; j++)
                {
                    bool removeTriangle = false;
                    Point vertex = triangle.Vertices[j];
                    for (int s = 0; s < supraTriangle.Vertices.Length; s++)
                    {
                        if (vertex.Equals(supraTriangle.Vertices[s]))
                        {
                            removeTriangle = true;
                            break;
                        }
                    }

                    if (removeTriangle)
                    {
                        triangles.RemoveAt(i);
                        break;
                    }
                }
            }


            return triangles;
        }
    }
}