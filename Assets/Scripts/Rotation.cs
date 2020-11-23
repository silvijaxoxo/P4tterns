using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation {

    private int startingPositionDegreesOdd; // pradinis rašto posūkio kampas nelyginėse eilutėse
    private bool x1Mirror;
    private bool y1Mirror;
    private int startingPositionDegreesEven; // pradinis rašto posūkio kampas nelyginėse eilutėse
    private bool x2Mirror;
    private bool y2Mirror;

    private Vector3 rot = new Vector3(0, 0, 0);

    public Rotation()
    {
        //nustatomi pasukimo parametrai
        startingPositionDegreesOdd = GetRandomRotation(new int[] { 0, 90, 180, 270 });
        x1Mirror = GetRandomBoolean();
        y1Mirror = GetRandomBoolean();
        startingPositionDegreesEven = startingPositionDegreesOdd + GetRandomRotation(new int[] { 0, 180 });
        x2Mirror = GetRandomBoolean();
        y2Mirror = GetRandomBoolean();
    }

    private bool GetRandomBoolean()
    {
        return Random.Range(0, 100) % 2 == 0;
    }

    private int GetRandomRotation(int[] values)
    {
        return values[Random.Range(0, values.Length)];
    }

    public Quaternion GetRotationXY(int x, int y, int degrees)
    {
        if (y % 2 == 0)
        {
            if (x2Mirror) // ar lyginėse eilutėse pasukti apie x ašį
                rot = new Vector3(rot.x + 180, rot.y, rot.z);
            if (x % 2 == 0)
            {
                if (y2Mirror)  // ar lyginiuose stulpeliuose pasukti apie y ašį
                    rot = new Vector3(rot.x, rot.y + 180, rot.z);
            }
            else
            {
                if (y1Mirror) // ar nelyginiuose stulpeliuose pasukti apie y ašį
                    rot = new Vector3(rot.x, rot.y + 180, rot.z);
            }
            rot = new Vector3(rot.x, rot.y, rot.z + startingPositionDegreesEven);
        }
        else
        {
            if (x1Mirror) // ar nelyginėse eilutėse pasukti apie x ašį
                rot = new Vector3(rot.x + 180, rot.y, rot.z);
            if (x % 2 == 0)
            {
                if (y2Mirror) // ar lyginiuose stulpeliuose pasukti apie y ašį
                    rot = new Vector3(rot.x, rot.y + 180, rot.z);
            }
            else
            {
                if (y1Mirror) // ar nelyginiuose stulpeliuose pasukti apie y ašį
                    rot = new Vector3(rot.x, rot.y + 180, rot.z);
            }
            rot = new Vector3(rot.x, rot.y, rot.z + startingPositionDegreesOdd);
        }
        rot = new Vector3(rot.x, rot.y, rot.z + degrees);

        Quaternion rotation = Quaternion.Euler(rot);
        rot = new Vector3(0, 0, 0);
        return rotation;
    }
}
