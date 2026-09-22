using Xunit;

namespace FindLocation.Tests;

public class Form1Tests
{
    private static string ExpectedPositionsText(Form1 form) => string.Join(Environment.NewLine,
        $"Location: X: {form.Location.X}, Y: {form.Location.Y}",
        $"DesktopLocation: X: {form.DesktopLocation.X}, Y: {form.DesktopLocation.Y}",
        $"Bounds: X: {form.Bounds.X}, Y: {form.Bounds.Y}, Width: {form.Bounds.Width}, Height: {form.Bounds.Height}",
        $"RestoreBounds: X: {form.RestoreBounds.X}, Y: {form.RestoreBounds.Y}",
        $"VirtualScreen: X: {SystemInformation.VirtualScreen.X}, Y: {SystemInformation.VirtualScreen.Y}, Width: {SystemInformation.VirtualScreen.Width}, Height: {SystemInformation.VirtualScreen.Height}",
        $"Screen Bounds: X: {Screen.FromControl(form).Bounds.X}, Y: {Screen.FromControl(form).Bounds.Y}, Width: {Screen.FromControl(form).Bounds.Width}, Height: {Screen.FromControl(form).Bounds.Height}",
        $"Working Area: X: {SystemInformation.WorkingArea.X}, Y: {SystemInformation.WorkingArea.Y}, Width: {SystemInformation.WorkingArea.Width}, Height: {SystemInformation.WorkingArea.Height}");

    [Fact]
    public void Form1_CanBeConstructed()
    {
        using var form = new Form1();

        Assert.NotNull(form);
    }

    [Fact]
    public void Form1_IsAWindowsFormsForm()
    {
        using var form = new Form1();

        Assert.IsAssignableFrom<Form>(form);
    }

    [Fact]
    public void Form1_HasPositionsLabelShowingInitialPositions()
    {
        using var form = new Form1();

        var label = Assert.IsType<Label>(form.Controls["positionsLabel"]);
        Assert.Equal(ExpectedPositionsText(form), label.Text);
    }

    [Fact]
    public void Form1_UpdatesPositionsLabelWhenFormMoves()
    {
        using var form = new Form1();

        form.Location = new Point(form.Location.X + 37, form.Location.Y + 41);

        var label = Assert.IsType<Label>(form.Controls["positionsLabel"]);
        Assert.Equal(ExpectedPositionsText(form), label.Text);
    }

    [Fact]
    public void Form1_UpdatesPositionsLabelWhenFormResizes()
    {
        using var form = new Form1();

        form.Size = new Size(form.Size.Width + 50, form.Size.Height + 60);

        var label = Assert.IsType<Label>(form.Controls["positionsLabel"]);
        Assert.Equal(ExpectedPositionsText(form), label.Text);
    }
}
