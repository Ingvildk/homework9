using Xamarin.Forms;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Http;
using static Week9PrismExampleApp.Models.WeatherItemModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Week9PrismExampleUnitTests")]
namespace Week9PrismExampleApp.ViewModels
{
    public class ListPageViewModel : BindableBase, INavigationAware
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

        private string _locationEnteredByUser;
        public string LocationEnteredByUser
        {
            get { return _locationEnteredByUser; }
            set { SetProperty(ref _locationEnteredByUser, value); }
        }

        public ObservableCollection<WeatherItem> _weatherCollection = new ObservableCollection<WeatherItem>();
        public ObservableCollection<WeatherItem> WeatherCollection
        {
            get { return _weatherCollection; }
            set { SetProperty(ref _weatherCollection, value); }
        }

        public DelegateCommand NavToNewPageCommand { get; set; }
        public DelegateCommand GetWeatherForLocationCommand { get; set; }
        public DelegateCommand<WeatherItem> DeleteCommand { get; set; }
        public DelegateCommand<WeatherItem> RedirectCommand{ get; set; }

        INavigationService _navigationService;

        public ListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavToNewPageCommand = new DelegateCommand(NavToNewPage);
            GetWeatherForLocationCommand = new DelegateCommand(GetWeatherForLocation);
            DeleteCommand = new DelegateCommand<WeatherItem>(Delete);
            RedirectCommand = new DelegateCommand<WeatherItem>(Redirect);

			Title = "Xamarin Forms Application + Prism";
            ButtonText = "Add Name";
        }

        internal async void GetWeatherForLocation()
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(
                string.Format(
                    $"https://jsonplaceholder.typicode.com/posts/{LocationEnteredByUser}"));
            var response = await client.GetAsync(uri);
            WeatherItem weatherData = null;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                weatherData = WeatherItem.FromJson(content);
            }
            WeatherCollection.Add(weatherData);
        }

        protected int FindMahIndex(int key, ObservableCollection<WeatherItem> col)
		{
			int index = 0;

			foreach (WeatherItem element in col)
			{
				if (key == element.id)
					return index;

				index++;
			}

			return -1;
		}

        public void Delete(WeatherItem weatherItem)
        {
			_weatherCollection.Remove(weatherItem);
		}


		private async void Redirect(WeatherItem weatherItem)
		{
			var navParams = new NavigationParameters();
			navParams.Add("WeatherItemInfo", weatherItem);
			await _navigationService.NavigateAsync("MoreInfoPage", navParams);
		}




        private async void NavToNewPage()
        {
            var navParams = new NavigationParameters();
            navParams.Add("NavFromPage", "ListPageViewModel");
            await _navigationService.NavigateAsync("SamplePageForNavigation", navParams);
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
    }
}

