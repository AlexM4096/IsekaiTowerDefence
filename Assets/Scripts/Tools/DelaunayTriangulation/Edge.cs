using System;
using UnityEngine;

namespace Tools.DelaunayTriangulation
{
    public class Edge : IEquatable<Edge>
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Vector2 Mid { get; private set; }

        public Edge(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public bool Equals(Edge other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Start.Value, other.Start.Value) && Equals(End.Value, other.End.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Edge)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End, Mid);
        }
    }
}