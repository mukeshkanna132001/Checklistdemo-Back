using ChecklistApi.Data;
using ChecklistApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChecklistApi.Controllers;

[ApiController] 
[Route("api/[controller]")]
//test test test 
public class ChecklistController : ControllerBase
{
    private readonly AppDbContext _db;
    public ChecklistController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChecklistItem>>> GetAll() =>
        await _db.ChecklistItems.OrderBy(i => i.Id).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<ChecklistItem>> Get(int id)
    {
        var item = await _db.ChecklistItems.FindAsync(id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<ChecklistItem>> Create(ChecklistItem item)
    {
        if (string.IsNullOrWhiteSpace(item.Title)) return BadRequest("Title is required.");
        item.Id = 0;
        item.CreatedAt = DateTime.UtcNow;
        _db.ChecklistItems.Add(item);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ChecklistItem input)
    {
        var item = await _db.ChecklistItems.FindAsync(id);
        if (item is null) return NotFound();
        item.Title = input.Title;
        item.IsCompleted = input.IsCompleted;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.ChecklistItems.FindAsync(id);
        if (item is null) return NotFound();
        _db.ChecklistItems.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
