using System.Windows;

namespace PickCardUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string[] pickedCards = CardPicker.PickSomeCards((int)numberOfCards.Value);
        listOfCards.Items.Clear();
        foreach (var card in pickedCards)
        {
            listOfCards.Items.Add(card);
        }
    }
}