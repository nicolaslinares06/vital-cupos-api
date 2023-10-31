using API;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit.API
{
    public class WeatherForecastTest
    {
        [Fact]
        public void WeatherForecast()
        {
            WeatherForecast datos = new WeatherForecast();
            datos.Date = DateTime.Now;
            datos.TemperatureC = 100;
            datos.Summary = "1";

            var type = Assert.IsType<WeatherForecast>(datos);
            Assert.NotNull(type);
        }

        [Fact]
        public void TemperatureF_CalculatedCorrectly()
        {
            // Arrange
            var weatherForecast = new WeatherForecast();

            // Act
            weatherForecast.TemperatureC = 0; // 0°C

            // Assert
            Assert.Equal(32, weatherForecast.TemperatureF);
        }

        [Fact]
        public void TemperatureF_RoundedCorrectly()
        {
            // Arrange
            var weatherForecast = new WeatherForecast();

            // Act
            weatherForecast.TemperatureC = 100; // 100°C

            // Assert
            Assert.Equal(212, weatherForecast.TemperatureF);
        }

        [Fact]
        public void TemperatureF_NegativeValue_CalculatedCorrectly()
        {
            // Arrange
            var weatherForecast = new WeatherForecast();

            // Act
            weatherForecast.TemperatureC = -10; // -10°C

            // Assert
            Assert.Equal(14, weatherForecast.TemperatureF);
        }

        [Fact]
        public void TemperatureF_DefaultValue()
        {
            // Arrange
            var weatherForecast = new WeatherForecast();

            // Act (no es necesario hacer nada, ya que el valor predeterminado se calcula automáticamente)

            // Assert
            Assert.Equal(32, weatherForecast.TemperatureF);
        }

        [Fact]
        public void Summary_SetAndGet()
        {
            // Arrange
            var weatherForecast = new WeatherForecast();
            string expectedSummary = "Sunny Day";

            // Act
            weatherForecast.Summary = expectedSummary;
            string actualSummary = weatherForecast.Summary;

            // Assert
            Assert.Equal(expectedSummary, actualSummary);
        }
    }
}
