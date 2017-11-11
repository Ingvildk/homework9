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
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand RedirectCommand{ get; set; }

        INavigationService _navigationService;

        public ListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavToNewPageCommand = new DelegateCommand(NavToNewPage);
            GetWeatherForLocationCommand = new DelegateCommand(GetWeatherForLocation);
            DeleteCommand = new DelegateCommand(Delete(object sender, System.EventArgs e));
            RedirectCommand = new DelegateCommand(Redirect);

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

		//delete
		void Handle_Clicked(object sender, System.EventArgs e)
		{

		}
        public async void Delete(object sender, System.EventArgs e)
        {
            MenuItem menI = (MenuItem)sender;
			int key = (int)menI.CommandParameter;
			//var col = (ObservableCollection<WeatherItem>) WeatherCollection.It;
			int actualIndex = FindMahIndex(key, _weatherCollection);

			_weatherCollection.RemoveAt(actualIndex);
        }

        public async void Redirect()
        {
			Device.OpenUri(new Uri("https://www.dllme.com/dll/files/system_device_dll.html"));
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

