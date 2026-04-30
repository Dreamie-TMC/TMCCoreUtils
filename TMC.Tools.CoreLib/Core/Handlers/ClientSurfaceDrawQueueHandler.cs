using BizHawk.Client.Common;
using TMC.Tools.CoreLib.Core.Synchronization;

namespace TMC.Tools.CoreLib.Core.Handlers;

public class ClientSurfaceDrawQueueHandler
{
    public ISynchronizedQueue<Action> ClientSurfaceQueue { get; internal set; } =
        new SynchronizedQueue<Action>();

    public const DisplaySurfaceID Id = DisplaySurfaceID.Client;

    public Action GetDrawAction()
    {
        var actions = new List<Action>();
        while (ClientSurfaceQueue.HasElement())
            actions.Add(ClientSurfaceQueue.Dequeue());

        return () =>
        {
            foreach (var action in actions)
                action.Invoke();
        };
    }
}
