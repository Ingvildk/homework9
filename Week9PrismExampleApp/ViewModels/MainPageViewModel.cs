using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Week9PrismExampleApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
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

        public MainPageViewModel()
        {
            Title = "Xamarin Forms Application + Prism";
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

