using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AbstractProduct
{
    protected GameParameters gameParameters;
    protected List<GameObject> patterns;
    protected float proporcH, proporcW, x1, x2;

    public void InstantiateScreenParameters()
    {
        proporcH = 7;
        proporcW = proporcH / Screen.height * Screen.width;
        Camera.main.orthographicSize = (float)(proporcH * 0.5);

        RectTransform panel = GameObject.Find("Panel").GetComponent<RectTransform>();
        RectTransform answerPanel = GameObject.Find("QuestionAnswerPanel").GetComponent<RectTransform>();
        x1 = proporcH * panel.rect.height * 0.48f * 0.5f / Screen.height;
        x2 = proporcH * answerPanel.rect.height * 0.48f * 0.5f / Screen.height;
    }

    public void Move()
    {
        foreach (GameObject pattern in patterns)
            pattern.transform.Rotate(0, 0, gameParameters.direction * 30 * (float)gameParameters.speed * Time.deltaTime);
    }
    public void Hide()
    {
        foreach (GameObject pattern in patterns)
            MonoBehaviour.Destroy(pattern.gameObject);
    }

    public bool CompareCount(AbstractProduct other)
    {
        return gameParameters.CompareCount(other.gameParameters);
    }
    public bool ComparePattern(AbstractProduct other)
    {
        return gameParameters.ComparePattern(other.gameParameters);
    }
    public bool CompareSize(AbstractProduct other)
    {
        return gameParameters.CompareSize(other.gameParameters);
    }
    public bool CompareDirection(AbstractProduct other)
    {
        return gameParameters.CompareDirection(other.gameParameters);
    }
    public bool CompareSpeed(AbstractProduct other)
    {
        return gameParameters.CompareSpeed(other.gameParameters);
    }
}

class ConcreteProduct1 : AbstractProduct
{
    public ConcreteProduct1(GameObject item)
    {
        InstantiateScreenParameters();
        float up = x2 + (proporcH - x1 - x2) / 2;
        Vector3 start = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(proporcW / 2, up, 0f);

        gameParameters = new GameParameters(1, item, RandomGameParameters.RandomSize(),
            RandomGameParameters.RandomDirection(), RandomGameParameters.RandomSpeed());
        item.transform.localScale = new Vector3((float)gameParameters.size, (float)gameParameters.size, 1f);
        patterns = new List<GameObject>
        {
            MonoBehaviour.Instantiate(item, start, item.transform.rotation)
        };
    }
}

class ConcreteProduct2 : AbstractProduct
{
    public ConcreteProduct2(GameObject item)
    {
        InstantiateScreenParameters();
        float up = x2 + (proporcH - x1 - x2) / 2;
        Vector3 start1 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(proporcW / 3, up, 0f);
        Vector3 start2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(2 * proporcW / 3, up, 0f);

        gameParameters = new GameParameters(2, item, RandomGameParameters.RandomSize(),
            RandomGameParameters.RandomDirection(), RandomGameParameters.RandomSpeed());
        item.transform.localScale = new Vector3((float)gameParameters.size, (float)gameParameters.size, 1f);
        patterns = new List<GameObject>
        {
            MonoBehaviour.Instantiate(item, start1, item.transform.rotation),
            MonoBehaviour.Instantiate(item, start2, item.transform.rotation)
        };
    }
}

class ConcreteProduct3 : AbstractProduct
{
    public ConcreteProduct3(GameObject item)
    {
        InstantiateScreenParameters();
        float up = (proporcH - x1 - x2) / 3;
        Vector3 start1 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(proporcW / 2, x2 + up, 0f);
        Vector3 start2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(proporcW / 3, x2 + 2 * up, 0f);
        Vector3 start3 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(2 * proporcW / 3, x2 + 2 * up, 0f);

        gameParameters = new GameParameters(3, item, RandomGameParameters.RandomSize(),
            RandomGameParameters.RandomDirection(), RandomGameParameters.RandomSpeed());
        item.transform.localScale = new Vector3((float)gameParameters.size, (float)gameParameters.size, 1f);
        patterns = new List<GameObject>
        {
            MonoBehaviour.Instantiate(item, start1, item.transform.rotation),
            MonoBehaviour.Instantiate(item, start2, item.transform.rotation),
            MonoBehaviour.Instantiate(item, start3, item.transform.rotation)
        };
    }
}

class ConcreteProduct4 : AbstractProduct
{
    public ConcreteProduct4(GameObject item)
    {
        InstantiateScreenParameters();
        float up = (proporcH - x1 - x2) / 3;

        Vector3 start1 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(proporcW / 3, x2 + up, 0f);
        Vector3 start2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(proporcW / 3, x2 + 2 * up, 0f);
        Vector3 start3 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(2 * proporcW / 3, x2 + up, 0f);
        Vector3 start4 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(2 * proporcW / 3, x2 + 2 * up, 0f);

        gameParameters = new GameParameters(4, item, RandomGameParameters.RandomSize(),
            RandomGameParameters.RandomDirection(), RandomGameParameters.RandomSpeed());
        item.transform.localScale = new Vector3((float)gameParameters.size, (float)gameParameters.size, 1f);
        patterns = new List<GameObject>
        {
            MonoBehaviour.Instantiate(item, start1, item.transform.rotation),
            MonoBehaviour.Instantiate(item, start2, item.transform.rotation),
            MonoBehaviour.Instantiate(item, start3, item.transform.rotation),
            MonoBehaviour.Instantiate(item, start4, item.transform.rotation)
        };
    }
}

abstract class AbstractFactory
{
    public abstract AbstractProduct CreateProduct1(GameObject item);
    public abstract AbstractProduct CreateProduct2(GameObject item);
    public abstract AbstractProduct CreateProduct3(GameObject item);
    public abstract AbstractProduct CreateProduct4(GameObject item);
}

class ConcreteDisplayFactory : AbstractFactory
{
    public override AbstractProduct CreateProduct1(GameObject item)
    {
        return new ConcreteProduct1(item);
    }

    public override AbstractProduct CreateProduct2(GameObject item)
    {
        return new ConcreteProduct2(item);
    }

    public override AbstractProduct CreateProduct3(GameObject item)
    {
        return new ConcreteProduct3(item);
    }

    public override AbstractProduct CreateProduct4(GameObject item)
    {
        return new ConcreteProduct4(item);
    }
}
