using System;
using System.Threading;
using PickUpRecipe.Core;
using Xamarin.Forms;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PickUpRecipe.Core.Parsers;
using PickUpRecipe.Core.ParserSettings;
using Xamarin.Essentials;

namespace PickUpRecipe
{
	/// <summary>
	/// Главное окно.
	/// </summary>
	public partial class MainPage : ContentPage
	{
		/// <summary>
		/// Парсер сайта.
		/// </summary>
		private readonly ParserWorker<string[]> _parser;

		/// <summary>
		/// Парсер рецепта.
		/// </summary>
		private readonly ParserWorker<string[]> _recipeParser;

		/// <summary>
		/// Парсер ингредиентов.
		/// </summary>
		private readonly ParserWorker<string[]> _ingredientsParser;

		/// <summary>
		/// Страница
		/// </summary>
		private int _page;

		/// <summary>
		/// Конструктор.
		/// </summary>
		public MainPage()
		{
			InitializeComponent();
			_parser = new ParserWorker<string[]>(new RecipeSiteParser());
			_recipeParser = new ParserWorker<string[]>(new SiteWithDishParser());
			_ingredientsParser = new ParserWorker<string[]>(new IngredientsParser());

			_parser.OnNewData += Parser_OnNewData;
			_recipeParser.OnNewData += RecipeParser_OnNewData;
			_ingredientsParser.OnNewData += IngredientsParser_OnNewData;
		}

		private async void Parser_OnNewData(object arg1, string[] arg2)
		{
			var rec = new Random(DateTime.Now.Second).Next(0, arg2.Length - 1);
			LinkEntry.Text = arg2[rec];
			_recipeParser.ParserSettings = new SiteWithDishSetting(arg2[rec]);
			await _recipeParser.Start();
			_ingredientsParser.ParserSettings = new SiteWithDishSetting(arg2[rec]);
			await _ingredientsParser.Start();
		}

		private void RecipeParser_OnNewData(object arg1, string[] arg2)
		{
			NameRecipeLabel.Text = arg2[0];
			RecipeImage.Source = arg2[1];
		}

		private void IngredientsParser_OnNewData(object arg1, string[] arg2)
		{
			Ingredients.Children.Clear();
			foreach (var ingredient in arg2)
            {
                var grid = new Grid
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(25, GridUnitType.Absolute) },
                        new ColumnDefinition { Width = GridLength.Auto }
					}
				};

				var boxes = new CheckBox
				{
					Color = Color.DarkRed
				};
				var labels = new Label
				{
					FontSize = 18,
					VerticalTextAlignment = TextAlignment.Center,
					Text = ingredient,
                };

				grid.Children.Add(boxes,0,0);
                grid.Children.Add(labels,1,0);
				Ingredients.Children.Add(grid);
			}
		}

		private async void MainDishButton_Click(object sender, EventArgs e)
		{
			_page = new Random(DateTime.Now.Second).Next(1, 15);
			var str = $"https://www.carolinescooking.com/main/page/{_page}/";
			_parser.ParserSettings = new RecipeSiteSettings(str);
			await _parser.Start();

			Show();
		}

		private async void SideDishButton_Click(object sender, EventArgs e)
		{
			_page = new Random(DateTime.Now.Second).Next(1, 7);
			var str = $"https://www.carolinescooking.com/side/page/{_page}/";
			_parser.ParserSettings = new RecipeSiteSettings(str);
			await _parser.Start();

			Show();
		}

		private async void DessertButton_Click(object sender, EventArgs e)
		{
			_page = new Random(DateTime.Now.Second).Next(1, 5);
			var str = $"https://www.carolinescooking.com/dessert/page/{_page}/";
			_parser.ParserSettings = new RecipeSiteSettings(str);
			await _parser.Start();

			Show();
		}

		private async void SnackButton_Click(object sender, EventArgs e)
		{ 
			_page = new Random(DateTime.Now.Second).Next(1, 9);
			var str = $"https://www.carolinescooking.com/snack/page/{_page}/";
			_parser.ParserSettings = new RecipeSiteSettings(str);
			await _parser.Start();

			Show();
		}

		private async void DrinkButton_Click(object sender, EventArgs e)
		{
			_page = new Random(DateTime.Now.Second).Next(1, 4);
			var str = $"https://www.carolinescooking.com/drink/page/{_page}/";
			_parser.ParserSettings = new RecipeSiteSettings(str);
			await _parser.Start();

			Show();
		}

		private async void HolidayRecipeButton_Click(object sender, EventArgs e)
		{
			_page = new Random(DateTime.Now.Second).Next(1, 6);
			var str = $"https://www.carolinescooking.com/seasonal-recipes/holiday-ideas/page/{_page}/";
			_parser.ParserSettings = new RecipeSiteSettings(str);
			await _parser.Start();

			Show();
		}

		private async void RecordingButton_Click(object sender, EventArgs e)
		{
			var text = NameRecipeLabel.Text + "\n";

			if (LinkCheckBox.IsChecked)
			{
				text += LinkEntry.Text + "\n";
			}

			foreach (var child in Ingredients.Children)
			{
				var grid = child as Grid;
				var checkBox = grid.Children[0] as CheckBox;
				var label = grid.Children[1] as Label;
				if (checkBox.IsChecked)
				{
					text += label.Text + "\n";
				}
			}

			await Clipboard.SetTextAsync(text);
			var text2 = Clipboard.GetTextAsync().ToString();
			await DisplayAlert("Data saved", "Data saved to clipboard", "OK");
 
		}

		/// <summary>
		/// Показать элементы.
		/// </summary>
		private void Show()
		{
			SelectAllStackLayout.IsVisible = true;
			LinkEntry.IsVisible = true;
			NameRecipeLabel.IsVisible = true;
			RecipeImage.IsVisible = true;
			LinkStackLayout.IsVisible = true;
			RecordingButton.IsVisible = true;
			SelectAll.IsChecked = false;
		}

		private void SelectAll_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
		{
			foreach (var child in Ingredients.Children)
			{
				var grid = child as Grid;
				var checkBox = grid.Children[0] as CheckBox;
				checkBox.IsChecked = SelectAll.IsChecked;
			}
		}
	}
}
