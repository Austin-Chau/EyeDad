using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director{

    // Director class gives a way to pick a vector direction in enum 
    public enum Direction { Up, Down, ForwardZ, BackwardZ, ForwardX, BackwardX }

    public Direction direction;

    public Vector3 GetVectorDirection()
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector3.up;
            case Direction.Down:
                return Vector3.down;
            case Direction.ForwardX:
                return Vector3.right;
            case Direction.BackwardX:
                return Vector3.left;
            case Direction.ForwardZ:
                return Vector3.forward;
            case Direction.BackwardZ:
                return Vector3.back;
            default:
                return Vector3.zero;
        }
    }

    public string GetAxis()
    {
        switch(direction)
        {
            case Direction.Up:
            case Direction.Down:
                return "y";
            case Direction.ForwardX:
            case Direction.BackwardX:
                return "x";
            case Direction.ForwardZ:
            case Direction.BackwardZ:
                return "z";
            default:
                return "";
        }
    }

}
