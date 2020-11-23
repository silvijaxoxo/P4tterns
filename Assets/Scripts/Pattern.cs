using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern {

    GameObject pattern;
    float speed;
    string name;
    
    Vector3 theEnd;
    Vector3 theStart;

    public Pattern(GameObject item, Vector3 start, Vector3 end)
    {
        name = item.name;
        pattern = MonoBehaviour.Instantiate(item, start, item.transform.rotation);
        pattern.GetComponent<SpriteRenderer>().sortingOrder = FocusGame.orderInLayer;
        FocusGame.orderInLayer -= 2;
        speed = 20;
        theEnd = end;
        theStart = start;
    }

    public bool ArePatternsEqual(Pattern other)
    {
        if (pattern.name.Equals(other.GetPatternGameObject().name))
            return true;
        else
            return false;
    }

    public void MoveLeft()
    {
        float step = speed * Time.deltaTime;
        pattern.transform.position = Vector3.MoveTowards(pattern.transform.position, theEnd, step);
    }

    public GameObject GetPatternGameObject()
    {
        return pattern;
    }

    public bool ReachedTargetPosition()
    {
        if (pattern.transform.position.Equals(theEnd))
            return true;
        else
            return false;
    }

    public void SetNextVector(Vector3 next)
    {
        theStart = theEnd;
        theEnd = next;
    }

    public void Delete()
    {
        MonoBehaviour.Destroy(pattern.gameObject);
    }

    public Vector3 GetStartVector()
    {
        return theStart;
    }

    public Vector3 GetEndVector()
    {
        return theEnd;
    }

    public string GetName()
    {
        return name;
    }
}
