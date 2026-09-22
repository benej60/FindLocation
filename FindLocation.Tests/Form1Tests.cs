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
}
