using UnityEngine;
using TMPro;

public class EquationHandler : MonoBehaviour
{
    public TMP_InputField[] inputs;
    public TextMeshProUGUI[] numbers;
    public AudioManager audioManager;
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        AssignNumbers();
        SelectInput(0);
    }

    public void AssignNumbers()
    {
        char[] newTime = GameManager.Instance.GetNewTime();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].text = newTime[i].ToString();
        }
    }

    public void SelectInput(int i)
    {
        inputs[i].Select();
        inputs[i].ActivateInputField();
    }

    public void AddOperation(int i, string c)
    {
        audioManager.Play("Add");
        GameManager.Instance.ClearMessage();
        if (inputs[i].text.Length < 2)
        {
            inputs[i].text = inputs[i].text + c;
        }
        else
        {
            inputs[i].text = c;
        }
    }

    public void ClearInput(int i)
    {
        audioManager.Play("Delete");
        inputs[i].text = "";
    }
    
    public void ClearInputs()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].text = "";
        }
    }

    public string GetEquation()
    {
        return inputs[0].text + numbers[0].text + inputs[1].text + numbers[1].text + inputs[2].text + numbers[2].text +
               inputs[3].text + numbers[3].text + inputs[4].text;
    }

    public bool IsEqualUsed()
    {
        foreach (var t in inputs)
        {
            if (t.text.Contains("="))
            {
                return true;
            }
        }
        return false;
    }
}
