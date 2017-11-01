using NUnit.Framework;
using System;
using Week9PrismExampleApp.ViewModels;
using Prism.Navigation;
using Moq;

namespace Week9PrismExampleUnitTests
{
    [TestFixture]
    public class MainPageViewModelTests
    {
        MainPageViewModel mainPageViewModel;

        Mock<INavigationService> navigationServiceMock;


        [SetUp]
        public void Init()
        {
            navigationServiceMock = new Mock<INavigationService>();
            mainPageViewModel = new MainPageViewModel(navigationServiceMock.Object);
        }

        [Test]
        public void TestAddNameAddsItemToWeatherCollection()
        {
            //Arrange: create instance of view model (done in init method)
            //Act: Call our method under test (AddName)
            //mainPageViewModel.AddName();

            //Assert: New item in WeatherCollection

            Assert.Contains("Temp",
                            mainPageViewModel.WeatherCollection);
        }
    }
}
