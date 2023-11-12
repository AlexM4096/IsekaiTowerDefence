using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
            float minX = Mathf.Infinity;
            float minY = Mathf.Infinity;
            float maxX = Mathf.NegativeInfinity;
            float maxY = Mathf.NegativeInfinity;

            foreach (Point point in points)
            {
                if (minX > point.x)
                    minX = point.x;

                if (minY > point.y)
                    minY = point.y;
                
                if (maxX < point.x)
                    maxX = point.x;

                if (maxY < point.y)
                    maxY = point.y;
            }

            return new Rect()
            {
                xMin = minX,
                xMax = maxX,
                yMin = minY,
                yMax = maxY
            };
        }
        
        public static ICollection<Edge> Triangulate(List<Point> points)
        {
            HashSet<Edge> edgs = new HashSet<Edge>();

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
                List<Edge> polygon = new List<Edge>();
                for (int i = 0; i < badTriangles.Count; i++)
                {
                    Triangle triangle = badTriangles[i];
                    Edge[] edges = triangle.GetEdges();
        
                    for (int j = 0; j < edges.Length; j++)
                    {
                        bool rejectEdge = false;
                        for (int t = 0; t < badTriangles.Count; t++)
                        {
                            if (t != i && badTriangles[t].ContainsEdge(edges[j]))
                            {
                                rejectEdge = true;
                            }
                        }
        
                        if (!rejectEdge)
                        {
                            polygon.Add(edges[j]);
                        }
                    }
                }
                
                foreach (Triangle triangle in badTriangles)
                {
                    triangles.Remove(triangle);
                }
                
                foreach (var edge in polygon)
                {
                    Point pointA = new Point(new Vector2(point.x, point.y));
                    Point pointB = new Point(edge.Start.Value);
                    Point pointC = new Point(edge.End.Value);
                    
                    triangles.Add(new Triangle(pointA, pointB, pointC));
                }
            }
            
            for (int i = 0; i < triangles.Count; i++)
            {
                Triangle triangle = triangles[i];
        
                if (triangle.Vertices.Any(vertex => supraTriangle.Vertices.Any(vertex.Equals)))
                {
                    triangles.RemoveAt(i);
                }
            }

            foreach (Triangle triangle in triangles)
            {
                edgs.AddRange(triangle.GetEdges());
            }
            
            return edgs;
        }
    }
}