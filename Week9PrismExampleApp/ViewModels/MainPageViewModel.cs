using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Http;
using static Week9PrismExampleApp.Models.WeatherItemModel;

namespace Week9PrismExampleApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set { SetProperty(ref _buttonText, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<string> _collectionOfNames = new ObservableCollection<string>();
        public ObservableCollection<string> CollectionOfNames
        {
            get { return _collectionOfNames; }
            set { SetProperty(ref _collectionOfNames, value); }
        }

        public DelegateCommand NavToNewPageCommand { get; set; }
        public DelegateCommand AddNameCommand { get; set; }

		INavigationService _navigationService;

		public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AddNameCommand = new DelegateCommand(AddName);
            NavToNewPageCommand = new DelegateCommand(NavToNewPage);
            Title = "Xamarin Forms Application + Prism";
            ButtonText = "Add Name";

			PopulateListView();
		}

        private async void NavToNewPage()
        {
            var navParams = new NavigationParameters();
            navParams.Add("NavFromPage", "MainPageViewModel"); 
            await _navigationService.NavigateAsync("SamplePageForNavigation", navParams);
        }

        private async void AddName()
        {
            CollectionOfNames.Add("Temp");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private async void PopulateListView()
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format($"http://api.openweathermap.org/data/2.5/weather?q=Escondido&APPID={ApiKeys.WeatherKey}"));
			var response = await client.GetAsync(uri);
            WeatherItem weatherData = null;
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
                weatherData = WeatherItem.FromJson(content);
			}
            CollectionOfNames.Add(weatherData.Name);

			CollectionOfNames.Add("Thomas");
            CollectionOfNames.Add("Charlie");
            CollectionOfNames.Add("James");
            CollectionOfNames.Add("Jennifer");
            CollectionOfNames.Add("Sharon");
            CollectionOfNames.Add("Aaron");
        }
    }
}

