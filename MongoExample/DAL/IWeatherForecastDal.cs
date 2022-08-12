using Microsoft.Extensions.Options;
using MongoDbDriver;
using MongoDbDriver.Repositories;

namespace MongoExample.DAL
{
    public interface IWeatherForecastDal : IGenericRepository<WeatherForecast, string>
    {
    }

    public class WeatherForecastMongoDbDal : MongoGenericRepository<WeatherForecast>, IWeatherForecastDal
    {
        public WeatherForecastMongoDbDal(IOptions<MongoDbSetting> options) : base(options)
        {
        }
    }
}
