using System;
using System.Data;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public EquationHandler eqHandler;
    public int selectedInput;
    public GameObject clearedLevels;
    public GameObject credits;
    public AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void MoveRight()
    {
        selectedInput++;
        MoveCursor();
    }
    public void MoveLeft()
    {
        selectedInput = eqHandler.inputs.Length + selectedInput - 1;
        MoveCursor();
    }
    
    public void MoveCursor()
    {
        audioManager.Play("Move");
        selectedInput = selectedInput % eqHandler.inputs.Length;
        eqHandler.SelectInput(selectedInput);
    }
    
    public void AddOperation(string operation)
    {
        eqHandler.AddOperation(selectedInput, operation);
    }
    
    public void AddEquals()
    {
        if (!eqHandler.IsEqualUsed())
        {
            eqHandler.AddOperation(selectedInput, "=");
        }
        else
        {
            GameManager.Instance.ErrorMessage("Use '=' only once");
        }
    }
    
    public void ClearInput()
    {
        GameManager.Instance.ClearMessage();
        eqHandler.ClearInput(selectedInput);
    }
    
    public void CheckEquation()
    {
        string equation = eqHandler.GetEquation();
        try
        {
            int result = (int)Convert.ToDouble(new DataTable().Compute(equation, null));
            if (result == 1)
            {
                audioManager.Play("Success");
                GameManager.Instance.LevelClear();
            }
            else
            {
                audioManager.Play("Error");
                GameManager.Instance.ErrorMessage("Equation is not true");
            }
        }
        catch (Exception e)
        {
            GameManager.Instance.ErrorMessage("Equation illegal");
            Debug.Log(e);
            throw;
        }
    }

    public void ShowClearedTimes()
    {
        audioManager.Play("Page");
        clearedLevels.SetActive(true);
        GameManager.Instance.UpdateClearedTimes();
    }
    
    public void HideClearedTimes()
    {
        audioManager.Play("Page");
        GameManager.Instance.GetNewTime();
        ResetGame();
        clearedLevels.SetActive(false);
    }
    
    public void ShowCredits()
    {
        audioManager.Play("Page");
        credits.SetActive(true);
    }
    
    public void HideCredits()
    {
        audioManager.Play("Page");
        GameManager.Instance.GetNewTime();
        ResetGame();
        credits.SetActive(false);
    }

    private void ResetGame()
    {
        eqHandler.ClearInputs();
        eqHandler.AssignNumbers();
        selectedInput = 0;
        eqHandler.SelectInput(selectedInput);
    }
}
