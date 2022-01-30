using TMPro;
using UnityEngine;
 
public class InputFieldWithoutKeyboard : TMP_InputField
{
    protected override void Start()
    {
        keyboardType = (TouchScreenKeyboardType)(-1);
        base.Start();
    }
}