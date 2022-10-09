using Connect.Widgets;
using SFML.Graphics;
using SFML.System;

namespace Connect.Utils
{
    internal class Line : Drawable
    {
        private List<Vector2f> _points;
        private VertexArray _vertices;
        private bool _isDirty;

        private Color _color;
        private float _thickness = 2;

        public Line()
        {
            _points = new();
            _color = Color.Black;
        }

        public IReadOnlyList<Vector2f> Points
        {
            get => _points.AsReadOnly();
        }

        public float Thickness
        {
            get => _thickness;
            set
            {
                if (_thickness != value)
                {
                    _thickness = value;
                    _isDirty = true;
                }
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    _isDirty = true;
                }
            }
        }

        public void AddPoint(float x, float y)
        {
            AddPoint(new Vector2f(x, y));
        }

        public void AddPoint(Vector2f point)
        {
            if (_points.Count > 0 && _points.Last() == point)
            {
                return;
            }

            _points.Add(point);
            _isDirty = true;
        }

        public void RemovePoint(float x, float y)
        {
            RemovePoint(new Vector2f(x, y));
        }

        public void RemovePoint(Vector2f point)
        {
            _points.Remove(point);
            _isDirty = true;
        }

        public void Clear()
        {
            _points.Clear();
            _isDirty = true;
        }

        public void Rebuild()
        {
            _vertices = new VertexArray(PrimitiveType.TriangleStrip);
            List<Vector2f> verts = new List<Vector2f>(_points.Count * 2);

            for (int i = 1; i < _points.Count; i++)
            {
                //Add previous point
                var currPoint = _points[i];
                var prevPoint = _points[i - 1];

                var segment = currPoint - prevPoint;
                var normal = segment.Normal().Normalized();

                verts.Add(prevPoint - Thickness * normal);
                verts.Add(prevPoint + Thickness * normal);

                //Correct seam
                if (i >= 2)
                {
                    var p2 = currPoint;
                    var p1 = prevPoint;
                    var p0 = _points[i - 2];

                    var seam = GetTangent(p0, p1, p2).Normal();
                    var seamLen = Thickness / seam.Dot(normal);


                    if (!float.IsNaN(seamLen))
                    {
                        verts[verts.Count - 2] = p1 - seamLen * seam;
                        verts[verts.Count - 1] = p1 + seamLen * seam;
                    }
                    else
                    {
                        verts[verts.Count - 2] = p1 + normal * (2f / Canvas.Spacing);
                        verts[verts.Count - 1] = p1 - normal * (2f / Canvas.Spacing);
                    }
                }

                //Add last element
                if (i == _points.Count - 1)
                {
                    verts.Add(currPoint - Thickness * normal);
                    verts.Add(currPoint + Thickness * normal);
                }
            }

            //Seam ends gżd
            if (IsLooping() && _points.Count >= 4)
            {
                var p2 = _points[1];
                var p1 = _points[0];
                var p0 = _points[_points.Count - 2];

                var segment = p1 - p0;
                var normal = segment.Normal().Normalized();

                var seam = GetTangent(p0, p1, p2).Normal();
                var seamLen = Thickness / seam.Dot(normal);

                verts[0] = p1 - seamLen * seam;
                verts[1] = p1 + seamLen * seam;

                verts[verts.Count - 2] = _points.Last() - seamLen * seam;
                verts[verts.Count - 1] = _points.Last() + seamLen * seam;
            }

            foreach (var vert in verts)
            {
                _vertices.Append(new Vertex(vert, Color));
            }

            _isDirty = false;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_isDirty)
            {
                Rebuild();
            }

            if (_vertices != null)
            {
                target.Draw(_vertices, states);
            }
        }

        private Vector2f GetTangent(Vector2f prevPoint, Vector2f point, Vector2f nextPoint)
        {
            return ((nextPoint - point).Normalized() + (point - prevPoint).Normalized()).Normalized();
        }

        private bool IsLooping()
        {
            return _points.First() == _points.Last();
        }
    }
}
