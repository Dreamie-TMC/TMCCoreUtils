using BizHawk.Client.Common;
using TMC.Tools.CoreLib.Core.Synchronization;

namespace TMC.Tools.CoreLib.Core.Handlers;

public class ClientSurfaceDrawQueueHandler
{
    public ISynchronizedQueue<Action> ClientSurfaceQueue { get; internal set; }

    public const DisplaySurfaceID Id = DisplaySurfaceID.Client;

    public ClientSurfaceDrawQueueHandler()
    {
        ClientSurfaceQueue = new SynchronizedQueue<Action>();
    }

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