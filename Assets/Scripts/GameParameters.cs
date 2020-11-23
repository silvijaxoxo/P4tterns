using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameParameters // naudojama Memory Mini-Game
{
    public int count;
    public GameObject ptrn;
    public double size;
    public int direction; // left, right
    public double speed;

    public GameParameters(int count, GameObject item, double size, int direction, double speed)
    {
        this.count = count;
        ptrn = item;
        this.size = size;
        this.direction = direction;
        this.speed = speed;
    }

    public bool CompareCount(GameParameters other)
    {
        if (this.count == other.count)
            return true;
        else return false;
    }
    public bool ComparePattern(GameParameters other)
    {
        if (this.ptrn.name.Equals(other.ptrn.name))
            return true;
        else return false;
    }
    public bool CompareSize(GameParameters other)
    {
        if (this.size == other.size)
            return true;
        else return false;
    }
    public bool IsSizeBigger(GameParameters other)
    {
        if (this.size > other.size)
            return true;
        else return false;
    }
    public bool CompareDirection(GameParameters other)
    {
        if (this.direction == other.direction)
            return true;
        else return false;
    }
    public bool CompareSpeed(GameParameters other)
    {
        if (this.speed == other.speed)
            return true;
        else return false;
    }
    public bool IsSpeedBigger(GameParameters other)
    {
        if (this.speed > other.speed)
            return true;
        else return false;
    }
}

public static class RandomGameParameters
{
    public static double RandomSize()
    {
        double[] values = new double[] { 0.8, 1, 1.2 };
        return values[Random.Range(0, values.Length)];
    }

    public static double RandomSpeed()
    {
        double[] values = new double[] { 0.5, 1, 2 };
        return values[Random.Range(0, values.Length)];
    }

    public static int RandomDirection()
    {
        double temp = Random.Range(0, 100);
        if (temp % 2 == 0)
            return 1;
        else return -1;
    }
}
