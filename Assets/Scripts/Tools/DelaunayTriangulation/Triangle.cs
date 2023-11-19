using UnityEngine;

namespace Tools.DelaunayTriangulation
{
    public class Triangle
    {
        public Point[] Vertices { get; } = new Point[3];
        
        public Point VertexA => Vertices[0];
        public Point VertexB => Vertices[1];
        public Point VertexC => Vertices[2];
        
        public Edge[] Edges { get; }
        
        public Vector2 CircumCentre { get; }
        public float CircumRadius { get; }

        public Triangle(Point pointA, Point pointB, Point pointC)
        {
            bool isCounterClockwise = IsCounterClockwise(pointA, pointB, pointC);

            Vertices[0] = pointA;
            Vertices[1] = isCounterClockwise ? pointB : pointC;
            Vertices[2] = isCounterClockwise ? pointC : pointB;

            CircumCentre = ComputeCircumCentre();
            CircumRadius = ComputeCircumRadius();
            
            Edges = new Edge[]
            {
                new(VertexA, VertexB),
                new(VertexB, VertexC),
                new(VertexC, VertexA),
            };
        }

        private static bool IsCounterClockwise(Point pointA, Point pointB, Point pointC)
        {
            float result = (pointB.x - pointA.x) * (pointC.y - pointA.y) - (pointC.x - pointA.x) * (pointB.y - pointA.y);
            return result > 0;
        }

        public bool ContainsEdge(Edge edge)
        {
            int sharedVerts = 0;
            for (int i = 0; i < Vertices.Length; i++)
            {
                if (Vertices[i].Equals(edge.Start) || Vertices[i].Equals(edge.End)){
                    sharedVerts++;
                }
            }
            return sharedVerts == 2;
        }

        public Vector2 ComputeCircumCentre()
        {

            Vector2 a = VertexA.Value;
            Vector2 b = VertexB.Value;
            Vector2 c = VertexC.Value;
            
            Vector2 sqrA = a * a;
            Vector2 sqrB = b * b;
            Vector2 sqrC = c * c;

            float d = (a.x * (b.y - c.y) + b.x * (c.y - a.y) + c.x * (a.y - b.y)) * 2f;
            float x = (sqrA.magnitude * (b.y - c.y) + sqrB.magnitude * (c.y - a.y) + sqrC.magnitude * (a.y - b.y)) / d;
            float y = (sqrA.magnitude * (c.x - b.x) + sqrB.magnitude * (a.x - c.x) + sqrC.magnitude * (b.x - a.x)) / d;
            
            return new Vector2(x, y);
        }

        public float ComputeCircumRadius()
        {
            Vector2 circumCentre = ComputeCircumCentre();
            return Vector2.Distance(circumCentre, Vertices[0]);
        }
    }
}