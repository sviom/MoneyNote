namespace MoneyNoteMAUI;

using MoneyNoteLibrary5.ViewModels;

public partial class MainPage : ContentPage
{
	int count = 0;

    public LoginViewModel ViewModel { get; set; }

    public MainPage()
	{
		InitializeComponent();
        this.Loaded += MainPage_Loaded;
	}

    private void MainPage_Loaded(object sender, EventArgs e)
    {
		ViewModel = new LoginViewModel();
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


