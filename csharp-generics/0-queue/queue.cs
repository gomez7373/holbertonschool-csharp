﻿using System;

/// <summary>
/// Generic Queue class
/// </summary>
public class Queue<T>
{
    /// <summary>
    /// Returns the type of the generic parameter
    /// </summary>
    public string CheckType()
    {
        return typeof(T).ToString();
    }
}

