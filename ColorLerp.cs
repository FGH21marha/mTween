using UnityEngine;

public class ColorLerp<T>
{
    Material mat;
    string matColor;
    SpriteRenderer spriteRend;

    public Color from;
    public Color to;

    float t;

    public ColorLerp(T type, string name)
    {
        matColor = name;
        UpdateMaterial(type);
    }

    public ColorLerp(T type) => UpdateMaterial(type);

    private void UpdateMaterial(T type)
    {
        if (type is Material)
            mat = type as Material;

        if (type is SpriteRenderer)
            spriteRend = type as SpriteRenderer;
    }

    public ColorLerp<T> SetColor(Color from, Color to)
    {
        this.from = from;
        this.to = to;
        return this;
    }

    public void Update()
    {
        t += Time.deltaTime;

        if (mat != null) mat.SetColor("_Color", LerpColor(from, to, t));
        if (spriteRend != null) spriteRend.color = LerpColor(from, to, t);
    }

    Color LerpColor(Color from, Color to, float t)
    {
        return Color.Lerp(from, to, t);
    }
}