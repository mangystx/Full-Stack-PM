namespace FSPM_1.Db.Models;

public class Video
{
	public int Id { get; set; }
	
	public string Query { get; set; } = string.Empty;
	
	public string VideoId { get; set; } = string.Empty;
	
	public string Title { get; set; } = string.Empty;
	
	public string ChannelTitle { get; set; } = string.Empty;
	
	public DateTime PublishedAt { get; set; }
	
	public string ThumbnailUrl { get; set; } = string.Empty;
}