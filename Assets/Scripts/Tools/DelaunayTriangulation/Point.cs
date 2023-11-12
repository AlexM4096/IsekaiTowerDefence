using UnityEngine;

namespace Tools.DelaunayTriangulation
{
    public class Point : Ref<Vector2>
    {
        public float x => Value.x;
        public float y => Value.y;
        
        public Point(Vector2 value) : base(value)
        {
        }
    }
}