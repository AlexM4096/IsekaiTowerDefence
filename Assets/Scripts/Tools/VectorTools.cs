using System;
using UnityEngine;

namespace Tools
{
    public enum Vector2To3Type
    {
        XY,
        XZ,
    }
    
    public static class VectorTools
    {
        public static Vector3 ToVector3(this Vector2 vector2, Vector2To3Type type = Vector2To3Type.XY)
        {
            switch (type)
            {
                case Vector2To3Type.XY:
                    return new Vector3(vector2.x, vector2.y, 0);
                
                case Vector2To3Type.XZ:
                    return new Vector3(vector2.x, 0, vector2.y);
                
                default:
                    throw new ArgumentException();
            }
        }

        public static Vector2 ToVector2(this Vector3 vector3, Vector2To3Type type = Vector2To3Type.XY)
        {
            switch (type)
            {
                case Vector2To3Type.XY:
                    return new Vector2(vector3.x, vector3.y);
                
                case Vector2To3Type.XZ:
                    return new Vector2(vector3.x, vector3.z);
                
                default:
                    throw new ArgumentException();
            }
        }
    }
}