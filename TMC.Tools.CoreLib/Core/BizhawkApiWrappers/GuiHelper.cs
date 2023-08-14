using BizHawk.Client.Common;

namespace TMC.Tools.CoreLib.Core.BizhawkApiWrappers;

/// <summary>
/// This class wraps bizhawk gui functions to abstract away the ApiContainer reference. Meant for use with DI
/// </summary>
public class GuiHelper
{
    internal ApiContainerWrapper ApiContainerWrapper { get; set; }
    
    public GuiHelper(IApiContainerWrapper apiContainerWrapper)
    {
        ApiContainerWrapper = (ApiContainerWrapper)apiContainerWrapper;
    }

    public void AddMessage(string message, int? duration = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.AddMessage(message, duration);

    public (int Left, int Top, int Right, int Bottom) GetPadding() =>
        ApiContainerWrapper.CurrentContainer.Gui.GetPadding();

    public void SetPadding(int l, int t, int r, int b) =>
        ApiContainerWrapper.CurrentContainer.Gui.SetPadding(l, t, r, b);

    public void SetPadding(int x, int y) =>
        ApiContainerWrapper.CurrentContainer.Gui.SetPadding(x, y);

    public void SetPadding(int all) =>
        ApiContainerWrapper.CurrentContainer.Gui.SetPadding(all);

    public void ClearGraphics(DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.ClearGraphics(surfaceId);

    public void ClearText() =>
        ApiContainerWrapper.CurrentContainer.Gui.ClearText();

    public void SetDefaultForegroundColor(Color color) =>
        ApiContainerWrapper.CurrentContainer.Gui.SetDefaultForegroundColor(color);

    public void SetDefaultBackgroundColor(Color color) =>
        ApiContainerWrapper.CurrentContainer.Gui.SetDefaultBackgroundColor(color);

    public Color? GetDefaultTextBackground() =>
        ApiContainerWrapper.CurrentContainer.Gui.GetDefaultTextBackground();

    public void SetDefaultTextBackground(Color color) =>
        ApiContainerWrapper.CurrentContainer.Gui.SetDefaultTextBackground(color);

    public void SetDefaultPixelFont(string fontfamily) =>
        ApiContainerWrapper.CurrentContainer.Gui.SetDefaultPixelFont(fontfamily);

    public void DrawBezier(Point p1, Point p2, Point p3, Point p4, Color? color = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawBezier(p1, p2, p3, p4, color, surfaceId);

    public void DrawBeziers(Point[] points, Color? color = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawBeziers(points, color, surfaceId);
    
    public void DrawBox(int x, int y, int x2, int y2, Color? line = null, Color? background = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawBox(x, y, x2, y2, line, background, surfaceId);
    
    public void DrawEllipse(int x, int y, int width, int height, Color? line = null, Color? background = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawEllipse(x, y, width, height, line, background, surfaceId);
    
    public void DrawIcon(string path, int x, int y, int? width = null, int? height = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawIcon(path, x, y, width, height, surfaceId);
    
    public void DrawImage(Image img, int x, int y, int? width = null, int? height = null, bool cache = true, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawImage(img, x, y, width, height, cache, surfaceId);
    
    public void DrawImage(string path, int x, int y, int? width = null, int? height = null, bool cache = true, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawImage(path, x, y, width, height, cache, surfaceId);

    public void ClearImageCache() =>
        ApiContainerWrapper.CurrentContainer.Gui.ClearImageCache();
    
    public void DrawImageRegion(Image img, int sourceX, int sourceY, int sourceWidth, int sourceHeight, int destX, int destY, int? destWidth = null, int? destHeight = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawImageRegion(img, sourceX, sourceY, sourceWidth, sourceHeight, destX, destY, destWidth, destHeight, surfaceId);
    
    public void DrawImageRegion(string path, int sourceX, int sourceY, int sourceWidth, int sourceHeight, int destX, int destY, int? destWidth = null, int? destHeight = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawImageRegion(path, sourceX, sourceY, sourceWidth, sourceHeight, destX, destY, destWidth, destHeight, surfaceId);

    public void DrawLine(int x1, int y1, int x2, int y2, Color? color = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawLine(x1, y1, x2, y2, color, surfaceId);

    public void DrawAxis(int x, int y, int size, Color? color = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawAxis(x, y, size, color, surfaceId);
    
    public void DrawPie(int x, int y, int width, int height, int startangle, int sweepangle, Color? line = null, Color? background = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawPie(x, y, width, height, startangle, sweepangle, line, background, surfaceId);

    public void DrawPixel(int x, int y, Color? color = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawPixel(x, y, color, surfaceId);
    
    public void DrawPolygon(Point[] points, Color? line = null, Color? background = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawPolygon(points, line, background, surfaceId);
    
    public void DrawRectangle(int x, int y, int width, int height, Color? line = null, Color? background = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawRectangle(x, y, width, height, line, background, surfaceId);
    
    public void DrawString(int x, int y, string message, Color? forecolor = null, Color? backcolor = null, int? fontsize = null, string? fontfamily = null, string? fontstyle = null, string? horizalign = null, string? vertalign = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawString(x, y, message, forecolor, backcolor, fontsize, fontfamily, fontstyle, horizalign, vertalign, surfaceId);
    
    [Obsolete("This method has been deprecated but is being kept for consistency")]
    public void DrawText(int x, int y, string message, Color? forecolor = null, Color? backcolor = null, string? fontfamily = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.DrawText(x, y, message, forecolor, backcolor, fontfamily, surfaceId);
    
    public void PixelText(int x, int y, string message, Color? forecolor = null, Color? backcolor = null, string? fontfamily = null, DisplaySurfaceID? surfaceId = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.PixelText(x, y, message, forecolor, backcolor, fontfamily, surfaceId);

    public void Text(int x, int y, string message, Color? forecolor = null, string? anchor = null) =>
        ApiContainerWrapper.CurrentContainer.Gui.Text(x, y, message, forecolor, anchor);
}