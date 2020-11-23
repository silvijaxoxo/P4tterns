using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemplateMethod : MonoBehaviour {

	protected Grid patternGrid;
    protected List<GameObject> patternList;
    protected GameObject ptrn;
    protected Rotation rot;
    protected int x, y;
    public abstract void ChangePattern();

    public void CreatePattern()
    {
        // parenkamas rašto indeksas iš masyvo
        int ind = Random.Range(0, patternList.Count);
        // pagal indeksą išrenkamas raštas
        ptrn = patternList[ind];
        ptrn.transform.localScale = new Vector3(1f, 1f, 1f);

        rot = new Rotation();

        for (int yy = 0; yy < patternGrid.GetHeight(); yy++)
        {
            for (int xx = 0; xx < patternGrid.GetWidth(); xx++)
            {
                ptrn.transform.rotation = rot.GetRotationXY(xx, yy, 0);
                ptrn.tag = "Untagged";
                patternGrid.Add(ptrn, xx, yy);
            }
        }

        // vieta, kur bus "ištrinamas"/"sudarkomas" rašto elementas
        x = Random.Range(0, patternGrid.GetWidth());
        y = Random.Range(0, patternGrid.GetHeight());

        ChangePattern();
    }

    public void CorrectPattern(RaycastHit2D hit, GameObject item)
    {
        Destroy(hit.transform.gameObject);
        patternGrid.Add(item, x, y);
    }
}
