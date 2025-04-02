using UnityEngine;
using UnityEngine.UI;

public class OnBoarding : MonoBehaviour
{
    [SerializeField] private Button[] _nextStepBtn;
    [SerializeField] private BoardStep[] _steps;
    private int _stepIndex;

    private void Start()
    {
        foreach (var i in _nextStepBtn)
        {
            i.onClick.AddListener(NextStep);
        }

        StartBoard();
    }

    public void StartBoard()
    {
        if (!ES3.KeyExists(Str.Board))
        {
            StartBoardWithoutKey();
        }
    }

    public void StartBoardWithoutKey()
    {
        _stepIndex = 0;
        //Time.timeScale = 0;
        _steps[_stepIndex].ActiveStep();
    }

    public void NextStep()
    {
        _stepIndex++;
        if (_stepIndex == _steps.Length - 1)
        {
            ES3.Save(Str.Board, 1);
            //Time.timeScale = 1;
        }

        _steps[_stepIndex].ActiveStep();
    }
}

[System.Serializable]
public class BoardStep
{
    [SerializeField] private GameObject[] on;
    [SerializeField] private GameObject[] off;

    public void ActiveStep()
    {
        foreach (var i in on)
            i.SetActive(true);

        foreach (var i in off)
            i.SetActive(false);
    }
}