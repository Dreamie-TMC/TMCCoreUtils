using TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

namespace TMC.Tools.CoreLib.Core.Handlers;

public interface IDrawHandler
{
    void Update();
}

public class DrawHandler(
    ClientSurfaceDrawQueueHandler clientSurfaceDrawQueueHandler,
    EmuSurfaceDrawQueueHandler emuSurfaceDrawQueueHandler,
    IApiContainerWrapper containerWrapper
) : IDrawHandler
{
    internal ClientSurfaceDrawQueueHandler ClientSurfaceDrawQueueHandler { get; set; } =
        clientSurfaceDrawQueueHandler;
    internal EmuSurfaceDrawQueueHandler EmuSurfaceDrawQueueHandler { get; set; } =
        emuSurfaceDrawQueueHandler;
    internal ApiContainerWrapper ApiContainerWrapper { get; set; } =
        (ApiContainerWrapper)containerWrapper;

    public void Update()
    {
        ApiContainerWrapper.CurrentContainer.Gui.WithSurface(
            ClientSurfaceDrawQueueHandler.Id,
            ClientSurfaceDrawQueueHandler.GetDrawAction()
        );
        ApiContainerWrapper.CurrentContainer.Gui.WithSurface(
            EmuSurfaceDrawQueueHandler.Id,
            EmuSurfaceDrawQueueHandler.GetDrawAction()
        );
    }
}
