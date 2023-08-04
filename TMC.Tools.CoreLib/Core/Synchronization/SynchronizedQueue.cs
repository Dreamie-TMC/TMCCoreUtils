﻿namespace TMC.Tools.CoreLib.Core.Synchronization;

public interface ISynchronizedQueue<T> : IDisposable
{
    /// <summary>
    /// Gets the number of items in the queue
    /// </summary>
    int GetQueueCount();

    /// <summary>
    /// Adds an item to the queue
    /// </summary>
    /// <returns>The total number of elements in the queue after adding the passed in element</returns>
    int Enqueue(T item);

    /// <summary>
    /// Dequeues each element currently in the queue and returns them
    /// </summary>
    /// <returns>A list containing each element of the queue in order</returns>
    List<T> GetElementsInQueue();

    /// <summary>
    /// Returns the first element in the queue
    /// </summary>
    T Dequeue();

    
    /// <summary>
    /// Determines whether or not there is an element in the queue
    /// </summary>
    /// <returns>True if there is at least one element in the queue, false otherwise</returns>
    bool HasElement();
}

/// <summary>
/// A thread safe queue that wraps the native .net queue
/// </summary>
/// <typeparam name="T">The type to use for the underlying queue</typeparam>
public class SynchronizedQueue<T> : ISynchronizedQueue<T>
{
    private volatile Queue<T> _dispatchQueue;
    private volatile int _currentQueueSize;

    private Mutex _mutex;

    public SynchronizedQueue()
    {
        _dispatchQueue = new Queue<T>(16);
        _currentQueueSize = 0;
        _mutex = new Mutex();
    }

    ~SynchronizedQueue()
    {
        Dispose(false);
    }

    public int GetQueueCount()
    {
        _mutex.WaitOne();
        var count = _currentQueueSize;
        _mutex.ReleaseMutex();
        return count;
    }

    public int Enqueue(T item)
    {
        _mutex.WaitOne();
        _dispatchQueue.Enqueue(item);
        Interlocked.Add(ref _currentQueueSize, 1);
        var count = _currentQueueSize;
        _mutex.ReleaseMutex();
        return count;
    }

    public List<T> GetElementsInQueue()
    {
        var list = new List<T>();
        _mutex.WaitOne();
        for (; 0 < _dispatchQueue.Count; )
        {
            list.Add(_dispatchQueue.Dequeue());
        }
        _mutex.ReleaseMutex();
        return list;
    }
    
    public T Dequeue()
    {
        _mutex.WaitOne();
        var result = _dispatchQueue.Dequeue();
        _mutex.ReleaseMutex();
        return result;
    }
    
    public bool HasElement()
    {
        _mutex.WaitOne();
        var result = _dispatchQueue.Count > 0;
        _mutex.ReleaseMutex();
        return result;
    }

    private void ReleaseUnmanagedResources()
    {
    }

    private void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();
        if (disposing)
        {
            _mutex.Dispose();
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}