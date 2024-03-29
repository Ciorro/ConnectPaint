﻿using LibTessDotNet;
using SFML.Graphics;
using SFML.System;

namespace Connect.Utils
{
    internal class Polygon : Drawable
    {
        private List<Vector2f> _points;
        private VertexArray _vertices;
        private bool _isDirty;

        private Color _color;

        public Polygon()
        {
            _points = new();
            _color = Color.Black;
        }

        public IReadOnlyList<Vector2f> Points
        {
            get => _points.AsReadOnly();
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
            if (_points.Count < 3)
            {
                return;
            }

            var countour = new ContourVertex[_points.Count];

            for (int i = 0; i < _points.Count; i++)
            {
                countour[i].Position = new Vec3()
                {
                    X = _points[i].X,
                    Y = _points[i].Y,
                    Z = 0
                };
                countour[i].Data = Color;
            }

            var tesselator = new Tess();
            tesselator.AddContour(countour);
            tesselator.Tessellate();

            _vertices = new VertexArray(
                type: PrimitiveType.Triangles,
                vertexCount: (uint)tesselator.ElementCount * 3
            );

            for (int i = 0; i < tesselator.ElementCount; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _vertices.Append(new Vertex()
                    {
                        Position = new Vector2f()
                        {
                            X = tesselator.Vertices[tesselator.Elements[i * 3 + j]].Position.X,
                            Y = tesselator.Vertices[tesselator.Elements[i * 3 + j]].Position.Y
                        },
                        Color = Color
                    });
                }
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
    }
}
