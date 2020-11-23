using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flyweight interface
public interface PatternElement {

    GameObject GetPattern();
    void MovePattern(GameObject ptrn, Vector3 vector);
}

// realizuoja Flyweight
public class PatternImp : PatternElement
{
    // vidinė būsena
    private GameObject pattern;
    private float speed = 5;

    public PatternImp(GameObject item)
    {
        pattern = item;
    }

    public GameObject GetPattern()
    {
        return pattern;
    }

    public void MovePattern(GameObject ptrn, Vector3 vector) //reikia istrinti objekta pradinej pozicijoj ir atvaizduoti ji kitoje
    {
        float step = speed * Time.deltaTime;
        ptrn.transform.position = Vector3.MoveTowards(ptrn.transform.position, vector, step);
    }
}

// sukuriami Flyweight objektai
public class PatternFactory
{
    private static PatternElement PATTERN;
    // gali but keli rastu tipai
    private static List<PatternElement> patterns;

    /*public static PatternElement getPattern(GameObject item)
    {
        if(PATTERN == null)
        {
            PATTERN = new PatternImp(item);
        }
        return PATTERN;
    }*/

    public static PatternElement getPattern(GameObject item)
    {
        if (patterns == null)
        {
            patterns = new List<PatternElement>();
            patterns.Add(new PatternImp(item));
        }
        else if(!patterns.Contains(new PatternImp(item)))   //jeigu dar toks rastas nebuvo idetas, jis pridedamas i lista
        {
            patterns.Add(new PatternImp(item));
        }
        // turi but grazinamas elementas, kurio gameobject sutampa su item
        return patterns.Find(x => x.GetPattern().name.Equals(item.name));
    }
}

public class PatternClient
{
    private PatternElement ptrn;
    private GameObject gameObject;

    private Vector3 start;
    private Vector3 end;

    public PatternClient(GameObject item, Vector3 theStart, Vector3 theEnd)
    {
        start = theStart;
        end = theEnd;
        ptrn = PatternFactory.getPattern(item);
        gameObject = MonoBehaviour.Instantiate(ptrn.GetPattern(), start, ptrn.GetPattern().transform.rotation);
    }

    public void SetEndVector(Vector3 theEnd)
    {
        end = theEnd;
    }

    public void MovePattern()
    {
        ptrn.MovePattern(gameObject, end);
    }
}
