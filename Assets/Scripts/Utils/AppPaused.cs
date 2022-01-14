using System;
using System.Collections.Generic;
using UnityEngine;

public class AppPaused : MonoBehaviour
{
    public static AppPaused Instance { get; set; }
    public event EventHandler ValueChanged;
    
    private bool isPaused, isFocused;

    private void Awake()
    {
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    public bool IsPaused { 
        get
        {
            return isPaused;
        }
        private set
        {
            if (isPaused != value)
                ValueChanged?.Invoke(this, null);

            isPaused = value;

            Debug.Log($"IsPaused: {isPaused}");
        }
    }

    public bool IsFocused
    {
        get
        {
            return isFocused;
        }
        private set
        {
            if (isFocused != value)
                ValueChanged?.Invoke(this, null);

            isFocused = value;

            Debug.Log($"isFocused: {isFocused}");
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        IsFocused = hasFocus;

        if (!IsFocused)
        {
            GameManager.Instance?.SaveGame();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        IsPaused = pauseStatus;

        if (IsPaused)
        {
            GameManager.Instance?.SaveGame();
        }
    }
}


public class FixedSizeStack<T> : Stack<T>
{
    private int MaxNumber;

    public FixedSizeStack()
    {
    }

    public FixedSizeStack(int Limit)
        : base()
    {
        MaxNumber = Limit;
    }

    public new void Push(T obj)
    {
        if (Count < MaxNumber)
        {
            base.Push(obj);
        }
        else
        {
            throw new StackOverflowException();
        }
    }
}