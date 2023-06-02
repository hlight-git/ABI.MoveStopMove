using System;

public interface IResizable : IBoostable
{
    float Size { get; }
    void SetSize(float size);
}