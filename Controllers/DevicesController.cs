using a_webapi.Dto;
using a_webapi.Dto.Pagination;
using a_webapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

namespace a_webapi.Controllers;

[ApiController]
[Route("api/devices")]
public class DevicesController(DeviceService deviceService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var devices = await deviceService.GetAllAsync();
        return Ok(devices);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<DeviceDto>>> GetAll(
        [FromQuery] DeviceQueryDto query)
    {
        if(query.Page <1)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid pagination parameters",
                detail: "Page must be greater than or equal to 1.");
        
        if(query.PageSize < 1 || query.PageSize > 100)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid pagination parameters",
                detail: "PageSize must be between 1 and 100.");
        
        var devices = await deviceService.GetAllAsync(query);
        return Ok(devices);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var device = await deviceService.GetByIdAsync(id);

        if (device == null)
            return NotFound();

        return Ok(device);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDeviceDto deviceDto)
    {
        var device = await deviceService.CreateAsync(deviceDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = device.Id },
            device);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDeviceDto deviceDto)
    {
        var updated = await deviceService.UpdateAsync(id, deviceDto);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await deviceService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpGet("{id}/history")]
    public async Task<IActionResult> GetHistoryById(int id)
    {
        var deviceExists = await deviceService.ExistsAsync(id);
        if (!deviceExists)
            return NotFound($"Device with ID {id} not found.");

        var history = await deviceService.GetHistoryByDeviceIdAsync(id);
        return Ok(history);
    }
}