using System;
using UnityEngine;
using Zenject;

public class BubbleManager: IInitializable, IDisposable
{
    private FinishedGameContainer _finishedGameContainer;

    public Action OnLevelWin, OnLevelLose;

    public BubbleManager(FinishedGameContainer finishedGameContainer){
        _finishedGameContainer = finishedGameContainer;
    }

    private int _bubbleCount;

    public void AddBubble()
    {
        _bubbleCount++;
    }

    public void RemoveBubble()
    {
        _bubbleCount--;

        CheckWin();
    }

    private void CheckWin()
    {
        if(_bubbleCount <= 0)
        {
            OnLevelWin?.Invoke();
            _finishedGameContainer.ShowContainer(true);
        }
    }

    private void Lose()
    {
        _finishedGameContainer.ShowContainer(false);
    }

    public void Initialize()
    {
        OnLevelLose += Lose;
    }

    public void Dispose()
    {
        OnLevelLose -= Lose;
    }
}
