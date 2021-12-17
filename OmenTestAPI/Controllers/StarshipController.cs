using Microsoft.AspNetCore.Mvc;
using OmenModels;
using OmenModels.Interfaces;
using OmenTestAPI.Interfaces;

namespace OmenTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarshipController : ControllerBase
    {
        private readonly IOmenRepository _omenRepository;
        public StarshipController(IOmenRepository omenRepository)
        {
            _omenRepository = omenRepository;
        }

        [HttpPost("StarshipClass")]
        public IActionResult AddStarshipClass(StarshipClass starshipClass)
        {
            try
            {
                if(starshipClass == null)
                {
                    return BadRequest("Model was null.");
                }

                _omenRepository.AddBaseModel(starshipClass);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("StarshipHull")]
        public IActionResult AddStarshipHull(StarshipHull hull)
        {
            try
            {
                if (hull == null)
                {
                    return BadRequest("Model was null.");
                }

                _omenRepository.AddBaseModel(hull);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ShipModule")]
        public IActionResult AddStarshipHull(ShipModule module)
        {
            try
            {
                if (module == null)
                {
                    return BadRequest("Model was null.");
                }

                _omenRepository.AddBaseModel(module);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Starship")]
        public IActionResult AddOrUpdateStarship(Starship ship)
        {
            try
            {
                if (ship == null)
                {
                    return BadRequest("Model was null.");
                }

                _omenRepository.UpdateBaseModel(ship);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ShipModuleList")]
        public IActionResult GetShipModuleList()
        {
            try
            {
                return Ok(_omenRepository.GetBaseModelList<ShipModule>());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("StarshipClassList")]
        public IActionResult GetStarshipClassList()
        {
            try
            {
                return Ok(_omenRepository.GetBaseModelList<StarshipClass>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("StarshipHullList")]
        public IActionResult GetStarshipHullList()
        {
            try
            {
                return Ok(_omenRepository.GetBaseModelList<StarshipHull>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("StarshipList")]
        public IActionResult GetStarshipList()
        {
            try
            {
                return Ok(_omenRepository.GetStarshipList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}