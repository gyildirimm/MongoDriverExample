using Microsoft.AspNetCore.Mvc;
using MongoExample.DAL;

namespace MongoExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastDal weatherForecastDal;

        public WeatherForecastController(IWeatherForecastDal weatherForecastDal)
        {
            this.weatherForecastDal = weatherForecastDal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = weatherForecastDal.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await weatherForecastDal.GetByIdAsync(id);
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody] WeatherForecast data)
        {
            var result = weatherForecastDal.AddAsync(data).Result;
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] WeatherForecast data)
        {
            var result = weatherForecastDal.UpdateAsync(id, data).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var result = weatherForecastDal.DeleteAsync(id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return NoContent();
        }
    }
}