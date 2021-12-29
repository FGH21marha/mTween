using UnityEngine;
using System;

public class ColorLerp
{
    public Color from;
    public Color to;

    float t;

    public ColorLerp GetColor(Action<Color> color)
    {
        color?.Invoke(LerpColor());
        return this;
    }

    public ColorLerp SetColor(Color from, Color to)
    {
        this.from = from;
        this.to = to;
        return this;
    }

    public void Update()
    {
        t += Time.deltaTime;
    }

    Color LerpColor()
    {
        return Color.Lerp(from, to, t);
    }
}