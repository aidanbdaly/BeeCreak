namespace BeeCreak.Play.Services;

public sealed class MapService(int? seed = null)
{
    private readonly int seed = seed ?? Random.Shared.Next();

    public float[,] BuildHeightMap(int width, int height)
    {
        var random = new Random(seed);
        return GenerateHeightMap(width, height, random);
    }

    private static float[,] GenerateHeightMap(int width, int height, Random random)
    {
        const int octaves = 5;
        const float persistence = 0.5f;

        var baseNoise = new float[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                baseNoise[x, y] = (float)random.NextDouble();
            }
        }

        var perlin = GeneratePerlinNoise(baseNoise, octaves, persistence);

        var centerX = (width - 1) / 2f;
        var centerY = (height - 1) / 2f;
        var maxRadius = MathF.Min(centerX, centerY);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var nx = (x - centerX) / maxRadius;
                var ny = (y - centerY) / maxRadius;
                var distance = MathF.Sqrt(nx * nx + ny * ny);
                var t = Clamp(distance, 0f, 1f);
                var falloff = 1f - SmoothStep(0.35f, 1f, t);

                var h = perlin[x, y] * falloff;
                perlin[x, y] = Clamp(h, 0f, 1f);
            }
        }

        return perlin;
    }

    private static float[,] GeneratePerlinNoise(float[,] baseNoise, int octaveCount, float persistence)
    {
        var width = baseNoise.GetLength(0);
        var height = baseNoise.GetLength(1);

        var smoothNoise = new float[octaveCount][,];
        for (var i = 0; i < octaveCount; i++)
        {
            smoothNoise[i] = GenerateSmoothNoise(baseNoise, i);
        }

        var perlinNoise = new float[width, height];
        var amplitude = 1f;
        var totalAmplitude = 0f;

        for (var octave = octaveCount - 1; octave >= 0; octave--)
        {
            amplitude *= persistence;
            totalAmplitude += amplitude;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    perlinNoise[x, y] += smoothNoise[octave][x, y] * amplitude;
                }
            }
        }

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                perlinNoise[x, y] = perlinNoise[x, y] / totalAmplitude;
            }
        }

        return perlinNoise;
    }

    private static float[,] GenerateSmoothNoise(float[,] baseNoise, int octave)
    {
        var width = baseNoise.GetLength(0);
        var height = baseNoise.GetLength(1);

        var smoothNoise = new float[width, height];
        var samplePeriod = 1 << octave;
        var sampleFrequency = 1f / samplePeriod;

        for (var x = 0; x < width; x++)
        {
            var sampleX0 = (x / samplePeriod) * samplePeriod;
            var sampleX1 = (sampleX0 + samplePeriod) % width;
            var horizontalBlend = (x - sampleX0) * sampleFrequency;

            for (var y = 0; y < height; y++)
            {
                var sampleY0 = (y / samplePeriod) * samplePeriod;
                var sampleY1 = (sampleY0 + samplePeriod) % height;
                var verticalBlend = (y - sampleY0) * sampleFrequency;

                var top = Lerp(baseNoise[sampleX0, sampleY0], baseNoise[sampleX1, sampleY0], horizontalBlend);
                var bottom = Lerp(baseNoise[sampleX0, sampleY1], baseNoise[sampleX1, sampleY1], horizontalBlend);
                smoothNoise[x, y] = Lerp(top, bottom, verticalBlend);
            }
        }

        return smoothNoise;
    }

    private static float SmoothStep(float edge0, float edge1, float value)
    {
        var t = Clamp((value - edge0) / (edge1 - edge0), 0f, 1f);
        return t * t * (3f - 2f * t);
    }

    private static float Clamp(float value, float min, float max)
    {
        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value;
    }

    private static float Lerp(float a, float b, float t) => a + (b - a) * t;
}
