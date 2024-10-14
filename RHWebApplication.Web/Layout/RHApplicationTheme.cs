using MudBlazor;
using MudBlazor.Utilities;
using static MudBlazor.CategoryTypes;
using static MudBlazor.Colors;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RHWebApplication.Web.Layout;
public sealed class RHApplicationPalette : PaletteLight
{
    private RHApplicationPalette()
    {
        Primary = new MudColor("#003366");
        Secondary = new MudColor("#007BFF");
        Tertiary = new MudColor("#66B2FF");
        Background = new MudColor("#F2F2F2");
        AppbarBackground = new MudColor("#003366");
        DrawerBackground = new MudColor("#FFFFFF");
        Surface = new MudColor("#FFFFFF");
        TextPrimary = new MudColor("#000000");
        TextSecondary = new MudColor("#757575");
        Error = new MudColor("#f44336");
        Success = new MudColor("#4caf50");
        Warning = new MudColor("#ff9800");
        Info = new MudColor("#2196f3");
        Divider = new MudColor("#CCCCCC");
        TableStriped = new MudColor("#E0E0E0");
    }
public static RHApplicationPalette CreatePallete => new();
}
