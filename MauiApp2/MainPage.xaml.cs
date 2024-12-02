using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        private bool isBackgroundColorToggled = false;
        private int colorStepIndex = 0;
        private Color[] colors = { Colors.Red, Colors.Green, Colors.Blue };
        private Button[,] gridButtons = new Button[3, 3];

        public MainPage()
        {
            InitializeComponent();
            InitializeButtonGrid();
        }

        private void InitializeButtonGrid()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    var button = new Button
                    {
                        BackgroundColor = Colors.Gray,
                        Text = " ",
                        WidthRequest = 50,
                        HeightRequest = 50
                    };
                    button.Clicked += GridButton_Click;
                    gridButtons[row, col] = button;
                    buttonGrid.Add(button, col, row);
                }
            }
        }

        private void ColorChangeBtn_Click(object sender, EventArgs e)
        {
            colorChangeBtn.TextColor = Colors.YellowGreen;
            colorChangeBtn.BackgroundColor = Colors.Green;
        }

        private void BackgroundToggleBtn_Click(object sender, EventArgs e)
        {
            isBackgroundColorToggled = !isBackgroundColorToggled;
            this.BackgroundColor = isBackgroundColorToggled ? Colors.LightBlue : Colors.White;
        }

        private void SumBtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(entry1.Text, out int num1) && int.TryParse(entry2.Text, out int num2))
            {
                resultLabel.Text = $"Result: {num1 + num2}";
            }
            else
            {
                resultLabel.Text = "Invalid numbers!";
            }
        }

        private void DisableEntriesBtn_Click(object sender, EventArgs e)
        {
            entry1.IsEnabled = false;
            entry2.IsEnabled = false;
        }

        private void SwitchColorsBtn_Click(object sender, EventArgs e)
        {
            rect1.Color = colors[colorStepIndex % colors.Length];
            rect2.Color = colors[(colorStepIndex + 1) % colors.Length];
            rect3.Color = colors[(colorStepIndex + 2) % colors.Length];
            colorStepIndex++;
        }

        private void ValueSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            sliderEntry.Text = ((int)e.NewValue).ToString();
        }

        private void SliderEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(sliderEntry.Text, out int sliderValue) && sliderValue >= 0 && sliderValue <= 100)
            {
                valueSlider.Value = sliderValue;
            }
            else
            {
                sliderEntry.Text = ((int)valueSlider.Value).ToString();
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var newButton = new Button
            {
                Text = "Delete Me",
                BackgroundColor = Colors.LightGray
            };
            newButton.Clicked += (s, args) => buttonContainer.Children.Remove(newButton);
            buttonContainer.Children.Add(newButton);
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int row = Grid.GetRow(btn);
            int col = Grid.GetColumn(btn);

            ToggleButtonColor(btn);
            ToggleAdjacentButtonColor(row - 1, col);
            ToggleAdjacentButtonColor(row + 1, col);
            ToggleAdjacentButtonColor(row, col - 1);
            ToggleAdjacentButtonColor(row, col + 1);

            if (AllButtonsToggled())
            {
                DisplayAlert("", "You won!", "OK");
            }
        }

        private void ToggleButtonColor(Button btn)
        {
            btn.BackgroundColor = (btn.BackgroundColor == Colors.Gray) ? Colors.Yellow : Colors.Gray;
        }

        private void ToggleAdjacentButtonColor(int row, int col)
        {
            if (row >= 0 && row < 3 && col >= 0 && col < 3)
            {
                ToggleButtonColor(gridButtons[row, col]);
            }
        }

        private bool AllButtonsToggled()
        {
            foreach (var button in gridButtons)
            {
                if (button.BackgroundColor == Colors.Gray)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
