using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomSelector : MonoBehaviour
{
    public static AtomSelector instance;

    public int HighestStartingIndex = 3;

    public GameObject Atom;
    public GameObject NoPhysicsAtom;

    public static string[] elements = {"H","He","Li","Be","B","C","N","O","F","Ne","Na","Mg","Al","Si","P"};
    public static int[] weights = {1,4,7,9,11,12,14,16,19,20,23,24,26,28,31};
    [SerializeField] private Image _nextAtomImage;
    // [SerializeField] private Sprite[] _fruitSprites;

    public GameObject NextAtom { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        print("HELLO");
        instance.NextAtom = Instantiate(NoPhysicsAtom);
        PickNextAtom();
    }

    public GameObject PickRandomAtomForThrow()
    {
        int randomIndex = Random.Range(0, HighestStartingIndex + 1);

        GameObject randomAtom = NoPhysicsAtom;
        float r = (randomIndex+1)*307%256;
        float g = (randomIndex+1)*593%256;
        float b = (randomIndex+1)*811%256;
        randomAtom.GetComponent<SpriteRenderer>().color = new Color(r,g,b);
        float scale = weights[randomIndex] / 31f * 0.6f + 0.1f;
        randomAtom.transform.localScale = new Vector3(scale,scale,scale);
        randomAtom.GetComponent<SpriteIndex>().Index = randomIndex;
        randomAtom.GetComponentInChildren<TMPro.TMP_Text>().text = "He";
        randomAtom.GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition = new Vector3(0, 0, 0);
        return randomAtom;
    }

    public void PickNextAtom()
    {
        int randomIndex = Random.Range(0, HighestStartingIndex + 1);

        float r = (randomIndex+1)*307%256;
        float g = (randomIndex+1)*593%256;
        float b = (randomIndex+1)*811%256;
        print("rgb " + r + " " + g + " " + b);
        // print("color "+ NextAtom.GetComponent<SpriteRenderer>().color);
        // print("color2 "+ new Color(r,g,b));
        NextAtom.GetComponent<SpriteRenderer>().color = new Color(r,g,b);
        float scale = weights[randomIndex] / 31f * 0.6f + 0.1f;
        NextAtom.transform.localScale = new Vector3(scale,scale,scale);
        NextAtom.GetComponent<SpriteIndex>().Index = randomIndex;
        print("TEXT " + NextAtom.GetComponentInChildren<TMPro.TMP_Text>().text);
        NextAtom.GetComponentInChildren<TMPro.TMP_Text>().text = "He";
        NextAtom.GetComponentInChildren<TMPro.TMP_Text>().transform.localPosition = new Vector3(0, 0, 0);

        // _nextAtomImage.GetComponent<Image>().color = new Color(r,g,b);
    }
}
