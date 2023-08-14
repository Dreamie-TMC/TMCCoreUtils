using TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

namespace TMC.Tools.CoreLib.Core.Handlers;

public interface IDrawHandler
{
    void Update();
}

public class DrawHandler : IDrawHandler
{
    internal ClientSurfaceDrawQueueHandler ClientSurfaceDrawQueueHandler { get; set; }
    internal EmuSurfaceDrawQueueHandler EmuSurfaceDrawQueueHandler { get; set; }
    internal ApiContainerWrapper ApiContainerWrapper { get; set; }

    public DrawHandler(ClientSurfaceDrawQueueHandler clientSurfaceDrawQueueHandler, EmuSurfaceDrawQueueHandler emuSurfaceDrawQueueHandler, IApiContainerWrapper containerWrapper)
    {
        ClientSurfaceDrawQueueHandler = clientSurfaceDrawQueueHandler;
        EmuSurfaceDrawQueueHandler = emuSurfaceDrawQueueHandler;
        ApiContainerWrapper = (ApiContainerWrapper)containerWrapper;
    }

    public void Update()
    {
        ApiContainerWrapper.CurrentContainer.Gui.WithSurface(ClientSurfaceDrawQueueHandler.Id, ClientSurfaceDrawQueueHandler.GetDrawAction());
        ApiContainerWrapper.CurrentContainer.Gui.WithSurface(EmuSurfaceDrawQueueHandler.Id, EmuSurfaceDrawQueueHandler.GetDrawAction());
    }
}