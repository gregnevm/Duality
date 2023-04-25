

using UnityEngine;

public interface IMoveable
{
    float MovementSpeed { get; }
    void Move(Vector2 direction);
    void StopMoving();
}
