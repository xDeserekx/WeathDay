using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeathDay.UnitTest
{
    [TestClass]
    public class MainWindowTests
    {
        private MainWindow mainWindow;

        [TestMethod]
        public void TestGetWeather()
        {
            string cityName = "London";

            mainWindow = new MainWindow();

            mainWindow.getWeather(cityName);

            Assert.IsNotNull(mainWindow.lblCityName.Text);
            Assert.IsNotNull(mainWindow.lblCountryName.Text);
            Assert.IsNotNull(mainWindow.lblTemp.Text);
            Assert.IsNotNull(mainWindow.lblWindMain.Text);
            Assert.IsNotNull(mainWindow.lblDesMain.Text);
            Assert.IsNotNull(mainWindow.lblPressureMain.Text);
            Assert.IsNotNull(mainWindow.lblDateTimeMain.Text);
            Assert.IsNotNull(mainWindow.pictureMain.Image);
        }

        [TestMethod]
        public void TestGetForecast()
        {
            string cityName = "London";

            mainWindow = new MainWindow();

            mainWindow.getForcast(cityName);

            Assert.IsNotNull(mainWindow.lblDay.Text);
            Assert.IsNotNull(mainWindow.lblDes.Text);
            Assert.IsNotNull(mainWindow.lblTempDay.Text);
            Assert.IsNotNull(mainWindow.lblWind.Text);
            Assert.IsNotNull(mainWindow.lblPressure.Text);
            Assert.IsNotNull(mainWindow.lblDateTime.Text);
            Assert.IsNotNull(mainWindow.picture2.Image);
            Assert.IsNotNull(mainWindow.picture3.Image);
            Assert.IsNotNull(mainWindow.picture4.Image);
            Assert.IsNotNull(mainWindow.picture5.Image);
            Assert.IsNotNull(mainWindow.picture6.Image);
            Assert.IsNotNull(mainWindow.picture10.Image);
            Assert.IsNotNull(mainWindow.picture11.Image);
            Assert.IsNotNull(mainWindow.picture12.Image);
            Assert.IsNotNull(mainWindow.picture13.Image);
            Assert.IsNotNull(mainWindow.picture14.Image);
        }

        [TestMethod]
        public void TestSetIcon()
        {
            string iconID = "01d";

            mainWindow = new MainWindow();

            var icon = mainWindow.setIcon(iconID);

            Assert.IsNotNull(icon);
        }

        [TestMethod]
        public void TestGetDate()
        {
            double milliseconds = 1618629638;

            mainWindow = new MainWindow();

            var date = mainWindow.getDate(milliseconds);

            Assert.IsNotNull(date);
        }

        [TestMethod]
        public void TestGetWeather_WhenCityExists_ReturnsValidData()
        {
            string cityName = "London";

            mainWindow = new MainWindow();

            mainWindow.getWeather(cityName);

            Assert.IsNotNull(mainWindow.lblCityName.Text);
            Assert.IsNotNull(mainWindow.lblCountryName.Text);
            Assert.IsNotNull(mainWindow.lblTemp.Text);
            Assert.IsNotNull(mainWindow.lblWindMain.Text);
            Assert.IsNotNull(mainWindow.lblDesMain.Text);
            Assert.IsNotNull(mainWindow.lblPressureMain.Text);
            Assert.IsNotNull(mainWindow.lblDateTimeMain.Text);
            Assert.IsNotNull(mainWindow.pictureMain.Image);

        }

    }
}
