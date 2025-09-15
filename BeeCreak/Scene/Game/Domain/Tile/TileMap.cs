using BeeCreak.src.Models;
using Newtonsoft.Json;

namespace BeeCreak
{
    
    public class Grid<T>
    {
        private readonly T[] data;
    
        public int Width { get; }
    
        public int Height { get; }
    
        public Grid(int w, int h) { Width = w; Height = h; data = new T[w * h]; }
    
        [JsonConstructor]
        public Grid(int width, int height, T[] data)
        {
            Width = width;
            Height = height;
            this.data = data;
    
            if (data.Length != width * height)
                throw new ArgumentException($"Data length {data.Length} does not match grid size {width}x{height}.");
        }
    
        public T this[int x, int y]
        {
            get => data[x + y * Width];
            set => data[x + y * Width] = value;
        }
    
        public IEnumerable<T> Data => data;
    
        public IEnumerable<(int, int, T)> Enumerate()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    yield return (x, y, this[x, y]);
        }
    
        public Grid<TResult> Map<TResult>(Func<T, TResult> selector)
        {
            var result = new Grid<TResult>(Width, Height);
    
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    result[x, y] = selector(this[x, y]);
    
            return result;
        }
    }
    
    public class ReadOnlyGrid<T>
    {
        private readonly T[] data;
    
        public int Width { get; }
    
        public int Height { get; }
    
        public ReadOnlyGrid(int w, int h) { Width = w; Height = h; data = new T[w * h]; }
    
        public ReadOnlyGrid(int width, int height, T[] data)
        {
            Width = width;
            Height = height;
            this.data = data;
    
            if (data.Length != width * height)
                throw new ArgumentException($"Data length {data.Length} does not match grid size {width}x{height}.");
        }
    
        public T this[int x, int y] => data[x + y * Width];
    
        public IEnumerable<T> Data => data;
    
        public IEnumerable<(int, int, T)> Enumerate()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    yield return (x, y, this[x, y]);
        }
    
        public Grid<TResult> Map<TResult>(Func<T, TResult> selector)
        {
            var result = new Grid<TResult>(Width, Height);
    
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    result[x, y] = selector(this[x, y]);
    
            return result;
        }
    }
}
