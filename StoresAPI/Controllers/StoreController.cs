using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoresAPI.Application.DTOs;
using StoresAPI.Application.Interface;
using StoresAPI.Domain;

namespace StoresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController(IStoreApplication storeApplication) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] StoreDTO store)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdStore = await storeApplication.CreateStoreAsync(store);
                return CreatedAtAction(nameof(GetStore), new { id = createdStore.Id }, createdStore);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(int id)
        {
            try
            {
                var store = await storeApplication.GetStoreAsync(id);
                if (store == null)
                {
                    return NotFound();
                }
                return Ok(store);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] StoreDTO updatedStore)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var store = await storeApplication.UpdateStoreAsync(id, updatedStore);
                if (store == null)
                {
                    return NotFound();
                }
                return Ok(store);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            try
            {
                var success = await storeApplication.DeleteStoreAsync(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
