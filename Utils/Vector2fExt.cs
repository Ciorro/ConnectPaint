using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Utils
{
    internal static class Vector2fExt
    {
        public static float Length(this Vector2f v)
        {
            return MathF.Sqrt((v.X * v.X) + (v.Y * v.Y));
        }

        public static Vector2f Normal(this Vector2f v)
        {
            return new Vector2f(-v.Y, v.X);
        }

        public static Vector2f Normalized(this Vector2f v)
        {
            return v / v.Length();
        }

        public static float Dot(this Vector2f v1, Vector2f v2)
        {
            return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

        public static float Cross(this Vector2f v1, Vector2f v2)
        {
            return (v1.X * v2.Y) - (v1.Y * v2.X);
        }

        public static float DistanceSquared(this Vector2f v1, Vector2f v2)
        {
            return MathF.Abs((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y));
        }

        public static float Distance(this Vector2f v1, Vector2f v2)
        {
            return MathF.Sqrt(DistanceSquared(v1, v2));
        }
    }
}
