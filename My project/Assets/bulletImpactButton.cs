using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletImpactButton : MonoBehaviour
{
    private DecoratorUI decoratorUI;
    // Start is called before the first frame update
    void Start()
    {
        decoratorUI = GetComponentInParent<DecoratorUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click()
    {
        decoratorUI.changebulletImapctPrefab(GetComponent<Image>().color);
    }
}
