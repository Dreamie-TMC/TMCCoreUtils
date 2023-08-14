using BizHawk.Client.Common;
using TMC.Tools.CoreLib.Core.Synchronization;

namespace TMC.Tools.CoreLib.Core.Handlers;

public class EmuSurfaceDrawQueueHandler
{
    public ISynchronizedQueue<Action> EmuSurfaceQueue { get; internal set; }

    public const DisplaySurfaceID Id = DisplaySurfaceID.EmuCore;

    public EmuSurfaceDrawQueueHandler()
    {
        EmuSurfaceQueue = new SynchronizedQueue<Action>();
    }

    public Action GetDrawAction()
    {
        var actions = new List<Action>();
        while (EmuSurfaceQueue.HasElement())
            actions.Add(EmuSurfaceQueue.Dequeue());

        return () =>
        {
            foreach (var action in actions)
                action.Invoke();
        };
    }
}