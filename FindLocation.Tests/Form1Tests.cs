using Xunit;

namespace FindLocation.Tests;

public class Form1Tests
{
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
    public void Form1_HasLocationLabelShowingInitialLocation()
    {
        using var form = new Form1();

        var label = Assert.IsType<Label>(form.Controls["locationLabel"]);
        Assert.Equal($"X: {form.Location.X}, Y: {form.Location.Y}", label.Text);
    }

    [Fact]
    public void Form1_UpdatesLocationLabelWhenFormMoves()
    {
        using var form = new Form1();

        form.Location = new Point(form.Location.X + 37, form.Location.Y + 41);

        var label = Assert.IsType<Label>(form.Controls["locationLabel"]);
        Assert.Equal($"X: {form.Location.X}, Y: {form.Location.Y}", label.Text);
    }
}
