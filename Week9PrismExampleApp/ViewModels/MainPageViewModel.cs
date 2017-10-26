using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace Week9PrismExampleApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        INavigationService _navigationService;

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

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AddNameCommand = new DelegateCommand(AddName);
            NavToNewPageCommand = new DelegateCommand(NavToNewPage);
            Title = "Xamarin Forms Application + Prism";
            ButtonText = "Add Name";
        }

        private async void NavToNewPage()
        {
            await _navigationService.NavigateAsync("SamplePageForNavigation");
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
            PopulateListView();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        private void PopulateListView()
        {
            CollectionOfNames.Add("Thomas");
            CollectionOfNames.Add("Charlie");
            CollectionOfNames.Add("James");
            CollectionOfNames.Add("Jennifer");
            CollectionOfNames.Add("Sharon");
            CollectionOfNames.Add("Aaron");
        }
    }
}

