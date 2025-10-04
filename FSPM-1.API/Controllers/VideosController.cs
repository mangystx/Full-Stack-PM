using FSPM_1.Db.Context;
using FSPM_1.Db.Models;
using FSPM_1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FSPM_1.Controllers;

public class VideosController(Fsmp1DbContext db, YouTubeService yt) : ControllerBase
{
	[HttpGet("health")]
	public IActionResult Health()
	{
		return Ok("ok");
	}

	/// GET /api/search?q=dotnet&maxResults=10&save=true
	[HttpGet("search")]
	public async Task<IActionResult> Search([FromQuery] string q, [FromQuery] int maxResults = 10,
		[FromQuery] bool save = false)
	{
		if (string.IsNullOrWhiteSpace(q)) return BadRequest("Порожній q.");

		var data = await yt.SearchAsync(q, maxResults);

		var items = data.Items
			.Where(i => i.Id.VideoId != null)
			.Select(i => new Video
			{
				Query = q,
				VideoId = i.Id.VideoId!,
				Title = i.Snippet.Title ?? string.Empty,
				ChannelTitle = i.Snippet.ChannelTitle ?? string.Empty,
				PublishedAt = i.Snippet.PublishedAt,
				ThumbnailUrl = i.Snippet.Thumbnails?.Medium?.Url
				               ?? i.Snippet.Thumbnails?.High?.Url
				               ?? i.Snippet.Thumbnails?.Default?.Url
				               ?? string.Empty
			})
			.ToList();

		var saved = 0;
		if (save)
		{
			var existing = await db.Videos.Select(v => v.VideoId).ToListAsync();
			var toAdd = items.Where(v => !existing.Contains(v.VideoId)).ToList();
			if (toAdd.Count > 0)
			{
				db.Videos.AddRange(toAdd);
				saved = await db.SaveChangesAsync();
			}
		}

		return Ok(new { count = items.Count, saved, items });
	}

	/// GET /api/videos
	[HttpGet("videos")]
	public async Task<IActionResult> GetAll()
	{
		var list = await db.Videos.OrderByDescending(v => v.Id).ToListAsync();
		
		return Ok(list);
	}

	/// DELETE /api/videos
	[HttpDelete("videos")]
	public async Task<IActionResult> Clear()
	{
		db.Videos.RemoveRange(db.Videos);
		await db.SaveChangesAsync();
		
		return NoContent();
	}
}