using InteractiveCarousell_P.Resources.Localization;
using InteractiveCarousell_P.Services;
using System.Collections.ObjectModel;
using System.Globalization;
namespace InteractiveCarousell_P;

public partial class ICarousell : ContentPage
{

	public class CarousellItem
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
	}

	private CarouselView carouselView;

	private ObservableCollection<CarousellItem> items;
	private int position = 0;

	public ICarousell()
	{
		Title = "Karusell - Dünaamiline lisamine";

		items = new ObservableCollection<CarousellItem>()
		{
			new CarousellItem {Title = "Pasta Carbonara", Description = AppRes.CarbonaraDescription, ImageUrl = "smilecow.png"},
			new CarousellItem {Title = "Pizza Margherita", Description = AppRes.PizzaDescription, ImageUrl = "pastamargherita.png"},
			new CarousellItem {Title = "Tiramisu", Description = AppRes.TiramisuDescription, ImageUrl = "tiramisu.png"},
			new CarousellItem {Title = "Risotto alla Milanese", Description = AppRes.RisottoDescription, ImageUrl = "risotto.png"},
			new CarousellItem {Title = "Lasagne", Description = AppRes.LasagneDescription, ImageUrl = "garfield.png" }
		};

		carouselView = new CarouselView
		{
			ItemsSource = items,
			Loop = true,
			HeightRequest = 350,
			PeekAreaInsets = new Thickness(40, 0, 40, 0),
			ItemTemplate = new DataTemplate(() =>
			{
				var frame = new Frame()
				{
					CornerRadius = 15,
					HasShadow = true,
					Padding = 0,
					Margin = new Thickness(5),
					BackgroundColor = Colors.Black
				};

				var grid = new Grid();
				
				var image = new Image { Aspect = Aspect.AspectFill };

				image.SetBinding(Image.SourceProperty, "ImageUrl");
				
				var gradient = new BoxView
				{
					Background = new LinearGradientBrush
					{
						StartPoint = new Point(0, 1),
						EndPoint = new Point(0, 0),
						GradientStops = new GradientStopCollection
						{
							new GradientStop(Colors.Black.WithAlpha(0.7f), 0),
							new GradientStop(Colors.Transparent, 1)
						}
					},
					Opacity = 0.7
				};
				var label = new Label
				{
					TextColor = Colors.White,
					FontSize = 24,
					Margin = new Thickness(20),
					VerticalOptions = LayoutOptions.End,
					HorizontalOptions = LayoutOptions.Start
				};
				label.SetBinding(Label.TextProperty, "Title");
				var tap = new TapGestureRecognizer();
				tap.Tapped += async (s, e) =>
				{
					var tappedItem = ((Frame)s).BindingContext as CarousellItem;
					await DisplayAlert("Valisid:", tappedItem?.Description ?? "Tundamtu", "OK");
				};
				frame.GestureRecognizers.Add(tap);

				grid.Children.Add(image);
				grid.Children.Add(gradient);
				grid.Children.Add(label);

				frame.Content = grid;
				return frame;
			})
		};

		var indicatorView = new IndicatorView
		{
			IndicatorColor = Colors.Gray,
			SelectedIndicatorColor = Colors.Blue,
			HorizontalOptions = LayoutOptions.Center,
			Margin = new Thickness(0, 10)
		};

		carouselView.IndicatorView = indicatorView;

		// Automaatne kerimine
		Device.StartTimer(TimeSpan.FromSeconds(4), () =>
		{
			if (items.Count == 0)
			{
				return false;
			}

			position = (position + 1) % items.Count;
			carouselView.Position = position;
			
			return true; // jätkab taimerit
		});

		HorizontalStackLayout languageStack = new HorizontalStackLayout();

		Button CreateLanguageButton(string cultureCode)
		{
			var btn = new Button
			{
				Text = cultureCode,
				HeightRequest = 50,
				WidthRequest = 50,
				CornerRadius = 5
			};

			btn.Clicked += (s, e) =>
			{
				var culture = new CultureInfo(cultureCode);
				AppRes.Culture = culture;
				Thread.CurrentThread.CurrentUICulture = culture;

				LoadItems();
				carouselView.ItemsSource = null;
				carouselView.ItemsSource = items;
			};

			return btn;
		}


		var EstonianButton = CreateLanguageButton("et-EE");
		var EnglishButton = CreateLanguageButton("en-US");
		// var RussianButton = CreateLanguageButton("ru-RU");

		languageStack.Children.Add(EstonianButton);
		languageStack.Children.Add(EnglishButton);

		Content = new StackLayout
		{
			Padding = 20,
			Children =
			{
				languageStack,
				carouselView,
				indicatorView,
			}
		};
		// InitializeComponent();
	}

	private void LoadItems()
	{
		items = new ObservableCollection<CarousellItem>()
        {
            new CarousellItem {Title = "Pasta Carbonara", Description = AppRes.CarbonaraDescription, ImageUrl = "smilecow.png"},
            new CarousellItem {Title = "Pizza Margherita", Description = AppRes.PizzaDescription, ImageUrl = "pastamargherita.png"},
            new CarousellItem {Title = "Tiramisu", Description = AppRes.TiramisuDescription, ImageUrl = "tiramisu.png"},
            new CarousellItem {Title = "Risotto alla Milanese", Description = AppRes.RisottoDescription, ImageUrl = "risotto.png"},
            new CarousellItem {Title = "Lasagne", Description = AppRes.LasagneDescription, ImageUrl = "garfield.png" }
        };
	}
}