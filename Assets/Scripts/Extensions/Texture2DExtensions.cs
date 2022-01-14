using UnityEngine;

public static class Texture2DExtensions
{
    public static Sprite CreateSprite(this Texture2D texture)
        => Sprite.Create(
            texture, 
            new Rect(0.0f, 0.0f, texture.width, texture.height), 
            Vector2.one * 0.5f, 
            texture.width);

    public static Texture2D VerticalFlipTexture(this Texture2D tex)
    {
        Texture2D result = new Texture2D(tex.width, tex.height);

        int width = tex.width, height = tex.height;

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                result.SetPixel(x, height - y - 1, tex.GetPixel(x, y));

        result.Apply();

        return result;
    }
}
