using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace WeathDay
{
    public partial class MainWindow : Form
    {
        bool mouseDown;
        private Point offset;
        private readonly string _apiKey;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                string apiKeyFilePath = "apiKey.txt";
                if(!File.Exists(apiKeyFilePath))
                { 
                    AppClose();     
                }

                _apiKey = File.ReadAllText(apiKeyFilePath);


                getWeather("Polska");
                getForcast("Polska");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error ocurred: {ex.Message}","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppClose() ;
            }


        }

        public bool IsValidApiKey(string apiKey)
        {
            return !string.IsNullOrEmpty(apiKey) && apiKey.Length == 32;
        }
        public MainWindow(string apiKey) => _apiKey = apiKey;

         public void getWeather(string cityName)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&cnt=6", cityName, _apiKey);

                try
                {
                    var json = web.DownloadString(url);
                    var result = JsonConvert.DeserializeObject<weatherInfo.root>(json);
                    weatherInfo.root outPut = result;

                    string languageCode = "en-EN";
                    CultureInfo culture = new CultureInfo(languageCode);

                    if(outPut != null)
                    {
                    lblCityName.Text = string.Format("{0}", outPut.name);
                    lblCountryName.Text = string.Format("{0}", outPut.sys.country);
                    lblTemp.Text = string.Format("{0:F1} \u00B0" + "C", outPut.main.temp);
                    lblWindMain.Text = string.Format("{0} " + "km/h", outPut.wind.speed);
                    lblDesMain.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(outPut.weather[0].description));
                    lblPressureMain.Text = string.Format("{0} " + "hPa", outPut.main.pressure);
                    lblDateTimeMain.Text = string.Format("{0}", getDate(outPut.dt).ToString("d", culture));

                    pictureMain.Image = setIcon(outPut.weather[0].icon);
                    }
                    else
                    {
                        lblCityName.Text = "";
                        lblCountryName.Text = "";
                        lblTemp.Text = "";
                        lblWindMain.Text = "";
                        lblDesMain.Text = "";
                        lblPressureMain.Text = "";
                        lblDateTimeMain.Text = "";

                        pictureMain.Image = null;
                    }

                }
                catch (WebException ex)
                {
                    // Handle exceptions related to web requests
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse response = ex.Response as HttpWebResponse;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            MessageBox.Show("No results found for the given city/country. Please check the correctness of the city, country name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred while retrieving weather data. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void getForcast(string city)
        {
            int day = 11;

            
            using (WebClient web = new WebClient())
            {
                string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt={1}&APPID={2}", city, day, _apiKey);
                try
                {
                    var json = web.DownloadString(url);
                    var Object = JsonConvert.DeserializeObject<weatherForcast>(json);

                    weatherForcast forcast = Object;
                    string languageCode = "en-EN";
                    CultureInfo culture = new CultureInfo(languageCode);

                    // next days
                    lblDay.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[1].dt).ToString("dddd", culture))); //returning day
                    lblDes.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(forcast.list[1].weather[0].description)); //weather description
                    lblTempDay.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[1].temp.day); //weather temp
                    lblWind.Text = string.Format("{0} km/h", forcast.list[1].speed); //weather wind speed  
                    lblPressure.Text = string.Format("{0} " + "hPa", forcast.list[1].pressure); // weather pressure
                    lblDateTime.Text = string.Format("{0}", getDate(forcast.list[1].dt).ToString("d", culture)); // date 

                    lblDay2.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[2].dt).ToString("dddd", culture))); //returning day
                    lblDes2.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(forcast.list[2].weather[0].description)); //weather description
                    lblTempDay2.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[2].temp.day); //weather temp
                    lblWind2.Text = string.Format("{0} km/h", forcast.list[2].speed); //weather wind speed  
                    lblPressure2.Text = string.Format("{0} " + "hPa", forcast.list[2].pressure); // weather pressure
                    lblDateTime2.Text = string.Format("{0}", getDate(forcast.list[2].dt).ToString("d", culture)); // date 

                    lblDay3.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[3].dt).ToString("dddd", culture))); //returning day
                    lblDes3.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(forcast.list[3].weather[0].description)); //weather description
                    lblTempDay3.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[3].temp.day); //weather temp
                    lblWind3.Text = string.Format("{0} km/h", forcast.list[3].speed); //weather wind speed  
                    lblPressure3.Text = string.Format("{0} " + "hPa", forcast.list[3].pressure); // weather pressure
                    lblDateTime3.Text = string.Format("{0}", getDate(forcast.list[3].dt).ToString("d", culture)); // date 

                    lblDay4.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[4].dt).ToString("dddd", culture))); //returning day
                    lblDes4.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(forcast.list[4].weather[0].description)); //weather description
                    lblTempDay4.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[4].temp.day); //weather temp
                    lblWind4.Text = string.Format("{0} km/h", forcast.list[4].speed); //weather wind speed  
                    lblPressure4.Text = string.Format("{0} " + "hPa", forcast.list[4].pressure); // weather pressure
                    lblDateTime4.Text = string.Format("{0}", getDate(forcast.list[4].dt).ToString("d", culture)); // date 

                    lblDay5.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[5].dt).ToString("dddd", culture))); //returning day
                    lblDes5.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(forcast.list[5].weather[0].description)); //weather description
                    lblTempDay5.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[5].temp.day); //weather temp
                    lblWind5.Text = string.Format("{0} km/h", forcast.list[5].speed); //weather wind speed  
                    lblPressure5.Text = string.Format("{0} " + "hPa", forcast.list[5].pressure); // weather pressure
                    lblDateTime5.Text = string.Format("{0}", getDate(forcast.list[5].dt).ToString("d", culture)); // date 

                    lblDay10.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[6].dt).ToString("dddd", culture))); //returning day
                    lblTempDay10.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[6].temp.day); //weather temp
                    lblDateTime10.Text = string.Format("{0}", getDate(forcast.list[6].dt).ToString("d", culture)); // date 

                    lblDay11.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[7].dt).ToString("dddd", culture))); //returning day
                    lblTempDay11.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[7].temp.day); //weather temp
                    lblDateTime11.Text = string.Format("{0}", getDate(forcast.list[7].dt).ToString("d", culture)); // date 

                    lblDay12.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[8].dt).ToString("dddd", culture))); //returning day
                    lblTempDay12.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[8].temp.day); //weather temp
                    lblDateTime12.Text = string.Format("{0}", getDate(forcast.list[8].dt).ToString("d", culture)); // date 

                    lblDay13.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[9].dt).ToString("dddd", culture))); //returning day
                    lblTempDay13.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[9].temp.day); //weather temp
                    lblDateTime13.Text = string.Format("{0}", getDate(forcast.list[9].dt).ToString("d", culture)); // date 

                    lblDay14.Text = string.Format("{0}", culture.TextInfo.ToTitleCase(getDate(forcast.list[10].dt).ToString("dddd", culture))); //returning day
                    lblTempDay14.Text = string.Format("{0:F1} \u00B0" + "C", forcast.list[10].temp.day); //weather temp
                    lblDateTime14.Text = string.Format("{0}", getDate(forcast.list[10].dt).ToString("d", culture)); // date 

                    // weather icon
                    picture2.Image = setIcon(forcast.list[1].weather[0].icon);
                    picture3.Image = setIcon(forcast.list[2].weather[0].icon);
                    picture4.Image = setIcon(forcast.list[3].weather[0].icon);
                    picture5.Image = setIcon(forcast.list[4].weather[0].icon);
                    picture6.Image = setIcon(forcast.list[5].weather[0].icon);
                    picture10.Image = setIcon(forcast.list[6].weather[0].icon);
                    picture11.Image = setIcon(forcast.list[7].weather[0].icon);
                    picture12.Image = setIcon(forcast.list[8].weather[0].icon);
                    picture13.Image = setIcon(forcast.list[9].weather[0].icon);
                    picture14.Image = setIcon(forcast.list[10].weather[0].icon);
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse response = ex.Response as HttpWebResponse;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {

                        }

                    }

                }
                catch (Exception ex)
                {

                }
            }
        }


        public DateTime getDate(double millisecound)
        {
            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecound).ToLocalTime();

            return day;
        }

        public void AppClose()
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            panel8.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            panel9.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            panel7.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            panel3.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            AppClose();
        }

        public Image setIcon(string iconID)
        {
            string url = string.Format("http://openweathermap.org/img/w/{0}.png", iconID);
            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            using (var weatherIcon = response.GetResponseStream())
            {
                Image weatherImg = Bitmap.FromStream(weatherIcon);
                return weatherImg;
            }
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AppClose();
        }

        private void pictureSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                getWeather(txtSearch.Text);
                getForcast(txtSearch.Text);
            }
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {
            panelTop.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void mD_Event(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }


        private void mM_Event(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void mU_Event(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void mouseDown_(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Text != "")
                {
                    getWeather(txtSearch.Text);
                    getForcast(txtSearch.Text);
                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            panel4.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            panel6.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            panel5.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            panel10.BackColor = Color.FromArgb(150, 0, 0, 0);
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {
            panel11.BackColor = Color.FromArgb(150, 0, 0, 0);
        }


    }
}
