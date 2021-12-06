using System;
using System.Threading;
using PickUpRecipe.Core;
using PickUpRecipe.Core.RecipeSite;
using Xamarin.Forms;
using PickUpRecipe.Core.SiteWithDish;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PickUpRecipe
{
    public partial class MainPage : ContentPage
    {
        private ParserWorker<string[]> parser;
        private ParserWorker<string[]> recipeParser;
        private ParserWorker<string[]> ingredientsParser;

        private StackLayout stackLayout = new StackLayout();
        private FlexLayout[] flexLayout;
        private CheckBox[] boxes;
        private Label[] labels;

        int page;
        private int count;

        public MainPage()
        {
            InitializeComponent();
            parser = new ParserWorker<string[]>(new RecipeSiteParser());
            recipeParser = new ParserWorker<string[]>(new SiteWithDishParser());
            ingredientsParser = new ParserWorker<string[]>(new IngredientsParser());

            parser.OnNewData += Parser_OnNewData;
            recipeParser.OnNewData += RecipeParser_OnNewData;
            ingredientsParser.OnNewData += IngredientsParser_OnNewData;
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            int rec = new Random(DateTime.Now.Second).Next(0, 15);

            linkEntry.Text = arg2[rec];

            recipeParser.Settings = new SiteWithDishSetting(arg2[rec]);
            recipeParser.Start();

            ingredientsParser.Settings = new SiteWithDishSetting(arg2[rec]);
            ingredientsParser.Start();
        }

        private void RecipeParser_OnNewData(object arg1, string[] arg2)
        {
            nameRecipeLabel.Text = arg2[0];
            recipeImage.Source = arg2[1];
        }

        private void IngredientsParser_OnNewData(object arg1, string[] arg2)
        {
            count = arg2.Length;
            stackLayout.Children.Clear();
            ingredients.Children.Clear();

            flexLayout = new FlexLayout[count];
            boxes = new CheckBox[count];
            labels = new Label[count];

            for(int i = 0; i < count; i++)
            {
                flexLayout[i] = new FlexLayout();

                boxes[i] = new CheckBox();
                boxes[i].Color = Color.DarkRed;

                labels[i] = new Label();
                labels[i].FontSize = 18;
                labels[i].VerticalTextAlignment = TextAlignment.Center;
                labels[i].Text = arg2[i];

                flexLayout[i].Children.Add(boxes[i]);
                flexLayout[i].Children.Add(labels[i]);
                stackLayout.Children.Add(flexLayout[i]);
            }

            ingredients.Children.Add(stackLayout);
        }

        private void MainDishButton_Click(object sender, EventArgs e)
        {
            page = new Random(DateTime.Now.Second).Next(1, 15);
            string str = $"https://www.carolinescooking.com/main/page/{page}/";
            parser.Settings = new RecipeSiteSettings(str);
            parser.Start();

            Show();
        }

        private void SideDishButton_Click(object sender, EventArgs e)
        {
            page = new Random(DateTime.Now.Second).Next(1, 7);
            string str = $"https://www.carolinescooking.com/side/page/{page}/";
            parser.Settings = new RecipeSiteSettings(str);
            parser.Start();

            Show();
        }

        private void DessertButton_Click(object sender, EventArgs e)
        {
            page = new Random(DateTime.Now.Second).Next(1, 5);
            string str = $"https://www.carolinescooking.com/dessert/page/{page}/";
            parser.Settings = new RecipeSiteSettings(str);
            parser.Start();

            Show();
        }

        private void SnackButton_Click(object sender, EventArgs e)
        { 
            page = new Random(DateTime.Now.Second).Next(1, 9);
            string str = $"https://www.carolinescooking.com/snack/page/{page}/";
            parser.Settings = new RecipeSiteSettings(str);
            parser.Start();

            Show();
        }

        private void DrinkButton_Click(object sender, EventArgs e)
        {
            page = new Random(DateTime.Now.Second).Next(1, 4);
            string str = $"https://www.carolinescooking.com/drink/page/{page}/";
            parser.Settings = new RecipeSiteSettings(str);
            parser.Start();

            Show();
        }

        private void HolidayRecipeButton_Click(object sender, EventArgs e)
        {
            page = new Random(DateTime.Now.Second).Next(1, 6);
            string str = $"https://www.carolinescooking.com/seasonal-recipes/holiday-ideas/page/{page}/";
            parser.Settings = new RecipeSiteSettings(str);
            parser.Start();

            Show();
        }

        private async void RecordingButton_Click(object sender, EventArgs e)
        {
            string text = nameRecipeLabel.Text + "\n";

            if (linkCheckBox.IsChecked)
            {
                text += linkEntry.Text + "\n";
            }

            for (int i = 0; i < count; i++)
            {
                if (boxes[i].IsChecked)
                {
                    text += "\n" + labels[i].Text;
                }
            }

            Clipboard.SetTextAsync(text);
            string text2 = Clipboard.GetTextAsync().ToString();
            await DisplayAlert("Data saved", "Data saved to clipboard", "OK");
 
        }

        private void Show()
        {
            linkEntry.IsVisible = true;
            nameRecipeLabel.IsVisible = true;
            recipeImage.IsVisible = true;
            linkStackLayout.IsVisible = true;
            recordingButton.IsVisible = true;
        }
    }
}
