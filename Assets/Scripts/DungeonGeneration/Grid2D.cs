using System;

namespace DungeonGeneration
{
    public class Grid2D<T>
    {
        public readonly int Width;
        public readonly int Height;
    
        private readonly T[] _values;

        public T this[int x, int y]
        {
            get => _values[GetIndex(x, y)];
            set => _values[GetIndex(x, y)] = value;
        }

        public Grid2D(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception();
            
            Width = width;
            Height = height;

            _values = new T[width * height];
        }

        public int GetIndex(int x, int y) => y * Width + x;

        public bool IsOnGrid(int x, int y) => !(x < 0 || x >= Width || y < 0 || y >= Height);
    }
}
