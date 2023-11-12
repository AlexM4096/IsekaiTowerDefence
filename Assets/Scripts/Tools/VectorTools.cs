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
            Vector3 vector3;
            
            switch (type)
            {
                case Vector2To3Type.XY:
                    vector3 = new Vector3(vector2.x, vector2.y, 0);
                    break;
                
                case Vector2To3Type.XZ:
                    vector3 = new Vector3(vector2.x, 0, vector2.y);
                    break;
                
                default:
                    throw new ArgumentException();
            }

            return vector3;
        }
    }
}