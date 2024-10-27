﻿namespace SerhendKumbara.Core.HTTP;

public class DataResult<T> : Result
{
    public DataResult(T data, bool success, string message) : base(success, message)
    {
        Data = data;
    }

    public DataResult(T data, bool success) : base(success)
    {
        Data = data;
    }

    public T Data { get; }
}
