using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorUIView : MonoBehaviour
{
    [SerializeField]
    DecoratorUIController decoratorUIController;

    private void Update()
    {
        IsOpen();
    }

    public void IsOpen()
    {
        decoratorUIController.IsOpen(Input.GetKeyDown(KeyCode.B));
    }
}
