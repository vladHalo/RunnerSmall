using System;
using Core.Scripts.Tools.Extensions;
using Core.Scripts.Views;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneView : MonoBehaviour
{
    [SerializeField] private LoadSceneModel[] _loadSceneModels;

    private void Start()
    {
        _loadSceneModels.ForEach(x =>
            x.btnLoadScene.onClick.AddListener(() =>
            {
                _loadSceneModels.ForEach(y => y.btnLoadScene.interactable = false);
                x.btnLoadScene.transform.DOScale(0, .2f).OnComplete(() =>
                    LoadingManager.Instantiate.LoadScene(x.nameScene.ToString()));
            }));
    }

    [Serializable]
    private class LoadSceneModel
    {
        public Button btnLoadScene;
        public NameScene nameScene;
    }

    public enum NameScene
    {
        Load,
        Game1,
        Game2,
        Game3,
        Game4
    }
}