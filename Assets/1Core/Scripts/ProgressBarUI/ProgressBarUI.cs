using System.Collections.Generic;
using Core.Scripts.Tools.Extensions;
using Core.Scripts.Views;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform _progressBarRect;
    [SerializeField] private LoadBar _loadBar;
    [SerializeField] private Color _unpassedColor, _passedColor;
    [SerializeField] private List<Image> _markers;

    private int _indexMarker = 1;
    private int _totalMarkersCount;
    private float _markerStepSize;

    public UnityEvent OnProgressInitialized, OnProgressUpdated, OnStageProgressPassed, OnOverallProgressPassed;

    [Button]
    public void Init(int maxMarkers)
    {
        if (maxMarkers > _markers.Count)
        {
            Debug.LogError("Number of markers exceeds available slots");
            return;
        }

        _totalMarkersCount = maxMarkers;
        _markerStepSize = 1f / maxMarkers;

        _markers.ForEach(x => x.gameObject.SetActive(false));

        float xPosition = 0;

        for (int i = 0; i < maxMarkers; i++)
        {
            _markers[i].gameObject.SetActive(true);
            xPosition += _markerStepSize * _progressBarRect.rect.width;
            _markers[i].rectTransform.anchoredPosition = _markers[i].rectTransform.anchoredPosition.SetX(xPosition);
        }

        OnProgressInitialized?.Invoke();
    }

    [Button]
    public bool SetValue(float progress)
    {
        OnProgressUpdated?.Invoke();

        float pathProgress = Mathf.Lerp(_markerStepSize * (_indexMarker - 1), _markerStepSize * _indexMarker, progress);
        _loadBar.SetProgress(pathProgress);

        if (progress >= 1)
        {
            MarkCurrentMarkerAsCompleted();
        }

        return progress >= 1;
    }

    private void MarkCurrentMarkerAsCompleted()
    {
        _markers[_indexMarker - 1].color = _passedColor;
        _indexMarker++;

        if (_indexMarker > _totalMarkersCount)
        {
            OnOverallProgressPassed?.Invoke();
            Debug.Log("FinishWay");
            return;
        }

        Debug.Log("FinishStage");
        OnStageProgressPassed?.Invoke();
    }

    [Button]
    public void ResetProgress()
    {
        _loadBar.SetProgress(0);
        _markers.ForEach(x => x.color = _unpassedColor);
        _indexMarker = 1;
    }
}