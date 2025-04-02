using System.Collections.Generic;
using _1Core.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class SelectAndBuyDesign : MonoBehaviour
{
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private Button[] _btnBuy, _btnSelect;
    [SerializeField] private Sprite _selectImg, _selectedImg;
    [SerializeField] private List<int> _prices;

    private int _selectedBtn;

    private void Start()
    {
        if (ES3.KeyExists($"{Str.SelectedElement}"))
            _selectedBtn = ES3.Load<int>(Str.SelectedElement);

        SelectedElement(_selectedBtn);

        for (int i = 0; i < _btnBuy.Length; i++)
        {
            if (ES3.KeyExists($"{Str.IsBuy}{i}"))
                _btnBuy[i].gameObject.SetActive(false);
        }
    }

    public void CheckMoney(int index)
    {
        if (_moneyManager.Money >= _prices[index])
        {
            _moneyManager.Money -= _prices[index];
            ES3.Save($"{Str.IsBuy}{index}", true);
            _btnBuy[index].gameObject.SetActive(false);
        }
        else
        {
            _moneyManager.MoveTween();
            Debug.Log("Не вистачає грошей!");
        }
    }

    public void SelectedElement(int index)
    {
        for (int i = 0; i < _btnSelect.Length; i++)
        {
            _btnSelect[i].image.sprite = _selectImg;
            _btnSelect[i].interactable = true;
        }

        _btnSelect[index].image.sprite = _selectedImg;
        _btnSelect[index].interactable = false;
        _selectedBtn = index;
        ES3.Save($"{Str.SelectedElement}", _selectedBtn);
        GameManager.instance.SetElement(index);
    }
}