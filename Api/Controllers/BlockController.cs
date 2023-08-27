using Domain.Entities.Interfaces;
using Domain.Exceptions;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BlockController : ControllerBase
{
    private readonly ILogger<BlockController> _logger;
    private readonly IBlockRepository _blockRepository;

    public BlockController(ILogger<BlockController> logger, IBlockRepository blockRepository)
    {
        _logger = logger;
        _blockRepository = blockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string key)
    {
        try
        {
            return Ok(await _blockRepository.GetAsync(key));
        }
        catch (WebsiteKeyNotFoundException)
        {
            return NotFound("Key not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, null, key);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string key)
    {
        try
        {
            await _blockRepository.CreateAsync(key);
            return Ok();
        }
        catch (DuplicateKeyException)
        {
            return BadRequest("Duplicate key");
        }
        catch (InvalidCharactersInKeyException)
        {
            return BadRequest("Key should contain no whitespaces or special characters");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, null, key);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(string key, Guid sectionId, [FromBody] IBlock block)
    {
        try
        {
            await _blockRepository.Update(key, sectionId, block);
            return Ok();
        }
        catch (WebsiteKeyNotFoundException)
        {
            return NotFound("Key not found");
        }
        catch (InvalidBlockException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, null, key);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(string key, Guid sectionId)
    {
        try
        {
            await _blockRepository.RemoveSection(key, sectionId);
            return Ok();
        }
        catch (WebsiteKeyNotFoundException)
        {
            return NotFound("Key not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, null, key);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
