using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// controller
public class bulletImpactButtonController : MonoBehaviour
{
    private DecoratorUIController decoratorUI;
    // Start is called before the first frame update
    void Start()
    {
        decoratorUI = GetComponentInParent<DecoratorUIController>();
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
