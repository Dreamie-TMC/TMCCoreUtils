using TMC.Tools.CoreLib.Core.Synchronization;

namespace TMC.Tools.CoreLib.Core.Handlers;

/// <summary>
/// A wrapper for the SynchronizedQueue class to handle dispatching events written to the queue.
/// </summary>
public interface IDelayedProcessingQueueHandler
{
    /// <summary>
    /// Processes each item in the queue. This should be the final function called in the "UpdateAfter" method of your tool.
    /// </summary>
    void Update();
}

public class DelayedProcessingQueueHandler : IDelayedProcessingQueueHandler
{
    internal SynchronizedQueue<Func<bool>> DelayedProcessingQueue { get; set; }

    public DelayedProcessingQueueHandler(SynchronizedQueue<Func<bool>> delayedProcessingQueue)
    {
        DelayedProcessingQueue = delayedProcessingQueue;
    }

    public void Update()
    {
        while (DelayedProcessingQueue.HasElement())
        {
            var func = DelayedProcessingQueue.Dequeue();
            if (!func.Invoke())
                DelayedProcessingQueue.Enqueue(func);
        }
    }
}