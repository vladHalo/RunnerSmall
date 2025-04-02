using System;
using _1Core.Scripts;
using _1Core.Scripts.Game.Player.Stats;
using _1Core.Scripts.Game.Wall;
using _1Core.Scripts.Patterns.Creational.Singleton;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private MoneyManager _moneyManager;

    [SerializeField] private int _level;

    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            _levelText.text = $"Level: {_level}";
            _levelText.gameObject.SetActive(true);
        }
    }

    [SerializeField] private TextMeshProUGUI _levelText;

    [SerializeField] private FloatStatManager gameStat;
    public FloatStatManager bonusStat;

    public bool isSuperPower;

    [SerializeField] public StatModel[] _statModels;

    [Space] [Header("UI")] [SerializeField]
    private Image _imageElement;

    [SerializeField] private Sprite[] _elementSprites;

    [Space] [Header("Stats")] public FloatStatManager _currentElementStats;

    [SerializeField] private FloatStatManager[] _elementStats;

    [Space] [Header("Texture")] [SerializeField]
    private Material _materialPlayer;

    [SerializeField] private Texture[] _textures;

    public int selectedElementIndex = 0;

    private void Start()
    {
        UpdateFullState();
    }

    public void UpdateFullState()
    {
        for (var i = 0; i < _statModels.Length; i++)
        {
            _statModels[i].FullStat = gameStat.GetStatValue(((TypeWall)i).ToString()) +
                                      bonusStat.GetStatValue(((TypeWall)i).ToString()) +
                                      _currentElementStats.GetStatValue(((TypeWall)i).ToString());
        }
    }

    public void SetElement(int index)
    {
        _imageElement.sprite = _elementSprites[index];
        _imageElement.SetNativeSize();
        _imageElement.rectTransform.sizeDelta = new Vector2(
            _imageElement.rectTransform.sizeDelta.x / 2,
            _imageElement.rectTransform.sizeDelta.y / 2
        );

        selectedElementIndex = index;
        _materialPlayer.mainTexture = _textures[index];
        _currentElementStats = _elementStats[index];
        UpdateFullState();
    }

    public float GetValue(TypeWall typeWall) => _statModels[(int)typeWall].FullStat;
}

[Serializable]
public class StatModel
{
    private float _fullStat;

    public float FullStat
    {
        get => _fullStat;
        set
        {
            _fullStat = value;
            if (_isBorder)
            {
                if (_fullStat > _max) _fullStat = _max;
                if (_fullStat < _min) _fullStat = _min;
            }

            _statText.text = _fullStat.ToString("F2");
        }
    }

    [SerializeField] private TMP_Text _statText;
    [SerializeField] private bool _isBorder;
    [SerializeField, ShowIf("_isBorder")] private float _min, _max;
}