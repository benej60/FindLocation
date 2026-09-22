namespace FindLocation;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        UpdateLocationLabel();
        LocationChanged += (sender, e) => UpdateLocationLabel();
    }

    private void UpdateLocationLabel()
    {
        locationLabel.Text = $"X: {Location.X}, Y: {Location.Y}";
    }
}
