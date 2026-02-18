using AutoMapper;
using D39_SehirRehberi.API.Data;
using D39_SehirRehberi.API.Dtos;
using D39_SehirRehberi.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace D39_SehirRehberi.API.Controllers
{
    [Route("api/Cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        public IActionResult GetCities() 
        {
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesToReturn);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]City city) 
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok(city);
        }

        [HttpGet("detail")]
        public IActionResult GetCityById(int id)
        {
            var city = _appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }

        [HttpGet("Photos")]
        public ActionResult GetPhotoByCity(int cityId) 
        {
            var photos = _appRepository.GetPhotosByCity(cityId);
            return Ok(photos);
        }
    }
}
