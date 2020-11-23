using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {

    public Vector2 offset = new Vector2(-0.2f, -0.2f);

    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndShadow;

    private Transform transCaster;
    private Transform transShadow;

    public Material shadowMaterial;
    public Color shadowColor;

    void Start()
    {
        transCaster = transform;
        transShadow = new GameObject().transform;
        //transShadow.parent = transCaster;
        transShadow.SetParent(transCaster, false);
        transShadow.gameObject.name = "shadow";
        transShadow.localRotation = Quaternion.identity;

        sprRndCaster = GetComponent<SpriteRenderer>();
        sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        sprRndShadow.material = shadowMaterial;
        sprRndShadow.color = shadowColor;
        sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
        sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
    }

    void LateUpdate()
    {
        if (transShadow != null)
        {
            transShadow.position = new Vector2(transCaster.position.x + offset.x,
                transCaster.position.y + offset.y);
            sprRndShadow.sprite = sprRndCaster.sprite;
        }
    }
}
