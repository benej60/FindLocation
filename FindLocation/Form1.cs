namespace FindLocation;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        UpdatePositionsLabel();
        LocationChanged += (sender, e) => UpdatePositionsLabel();
        Resize += (sender, e) => UpdatePositionsLabel();
    }

    private void UpdatePositionsLabel()
    {
        var virtualScreen = SystemInformation.VirtualScreen;
        var workingArea = SystemInformation.WorkingArea;
        var screenBounds = Screen.FromControl(this).Bounds;

        positionsLabel.Text = string.Join(Environment.NewLine,
            $"Location: X: {Location.X}, Y: {Location.Y}",
            $"DesktopLocation: X: {DesktopLocation.X}, Y: {DesktopLocation.Y}",
            $"Bounds: X: {Bounds.X}, Y: {Bounds.Y}, Width: {Bounds.Width}, Height: {Bounds.Height}",
            $"RestoreBounds: X: {RestoreBounds.X}, Y: {RestoreBounds.Y}",
            $"VirtualScreen: X: {virtualScreen.X}, Y: {virtualScreen.Y}, Width: {virtualScreen.Width}, Height: {virtualScreen.Height}",
            $"Screen Bounds: X: {screenBounds.X}, Y: {screenBounds.Y}, Width: {screenBounds.Width}, Height: {screenBounds.Height}",
            $"Working Area: X: {workingArea.X}, Y: {workingArea.Y}, Width: {workingArea.Width}, Height: {workingArea.Height}");
    }
}
