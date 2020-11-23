using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    private static GameObject[,] grid;
    private int width;
    private int height;
    private Vector3 start;

    public Grid(int w, int h, double prop)
    {
        width = w;
        height = h;
        grid = new GameObject[width, height];
        start = CountStartVector(width, height, prop);
    }

    private Vector3 CountStartVector(int w, int h, double prop)
    {
        double proporcW = prop * w / ((double)Screen.height / Screen.width);
        double proporcH = proporcW * ((double)Screen.height / Screen.width);
        Camera.main.orthographicSize = (float)(proporcW * Screen.height / Screen.width * 0.5);
        float up = (float)((proporcH - h + 0.5) / 2);
        float right = (float)((proporcW - w) / 2);
        Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(right + 0.5f, up + 0.5f, 0f);
        Vector3 startVector = vector;

        return startVector;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public void Add(GameObject item, int x, int y)
    {
        if (grid[x, y] != null)
            Delete(x, y);
        grid[x, y] = MonoBehaviour.Instantiate(item, start + new Vector3(x, y, 0f), item.transform.rotation);
    }

    public void Delete(int x, int y)
    {
        if (grid[x, y] != null)
            MonoBehaviour.Destroy(grid[x, y].gameObject);
    }

    public Vector3 GetStartVector()
    {
        return start;
    }
}
