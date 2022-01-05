using Microsoft.AspNetCore.Mvc;
using OmenModels;
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
        public async Task<IActionResult> AddStarshipClass(StarshipClass starshipClass)
        {
            try
            {
                if(starshipClass == null)
                {
                    return BadRequest("Model was null.");
                }

                await _omenRepository.Create(starshipClass);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("StarshipHull")]
        public async Task<IActionResult> AddStarshipHull(StarshipHull hull)
        {
            try
            {
                if (hull == null)
                {
                    return BadRequest("Model was null.");
                }

                await _omenRepository.Create(hull);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ShipModule")]
        public async Task<IActionResult> AddOrUpdateShipModule(ShipModule module)
        {
            try
            {
                if (module == null)
                {
                    return BadRequest("Model was null.");
                }

                if(module.Id == null)
                {
                    await _omenRepository.Create(module);
                    return Ok(module);
                }
                else
                {
                    await _omenRepository.Replace(module);
                    await UpdateStarshipsForModuleChange(module);
                    return Ok(module);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Starship")]
        public async Task<IActionResult> AddOrUpdateStarship(Starship ship)
        {
            try
            {
                if (ship == null)
                {
                    return BadRequest("Model was null.");
                }

                if(ship.Id == null)
                {
                    List<Starship> duplicateStarships = await _omenRepository.GetStarshipByFilter(x => x.Name == ship.Name);

                    if (duplicateStarships.Any()) return BadRequest("Unable to create new ship. A ship with this name already exists.");

                    ship = await _omenRepository.Create(ship);
                    return Ok(ship);
                }
                else
                {
                    await _omenRepository.Replace(ship);
                    return Ok(ship);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Starship/{id}")]
        public async Task<IActionResult> DeleteStarship(string id)
        {
            try
            {
                await _omenRepository.DeleteStarshipById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ShipModuleList")]
        public async Task<IActionResult> GetShipModuleList()
        {
            try
            {
                List<ShipModule> modules = await _omenRepository.GetShipModuleListAsync();
                return Ok(modules);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("StarshipClassList")]
        public async Task<IActionResult> GetStarshipClassList()
        {
            try
            {
                List<StarshipClass> classes = await _omenRepository.GetStarshipClassListAsync();
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("StarshipHullList")]
        public async Task<IActionResult> GetStarshipHullList()
        {
            try
            {
                List<StarshipHull> hulls = await _omenRepository.GetStarshipHullListAsync();
                return Ok(hulls);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("StarshipList")]
        public async Task<IActionResult> GetStarshipList()
        {
            try
            {
                List<Starship> ships = await _omenRepository.GetStarshipListAsync();
                return Ok(ships);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task UpdateStarshipsForModuleChange(ShipModule module)
        {
            List<Starship> ships = await _omenRepository.GetStarshipListAsync();

            ships.ForEach(async ship =>
            {
                if (ship.Modules.Any(x => x.Id == module.Id))
                {
                    int removedModules = ship.Modules.RemoveAll(x => x.Id == module.Id);
                    for (int i = 0; i < removedModules; i++)
                    {
                        ship.Modules.Add(module);
                    }

                    await _omenRepository.Replace(ship);
                }
            });
        }
    }
}