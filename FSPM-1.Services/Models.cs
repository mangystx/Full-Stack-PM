using System.Text.Json.Serialization;

namespace FSPM_1.Services;

public class YoutubeSearchResponse
{
	[JsonPropertyName("items")] public List<YoutubeItem> Items { get; set; } = new();
}

public class YoutubeItem
{
	[JsonPropertyName("id")] public YoutubeId Id { get; set; } = new();
	[JsonPropertyName("snippet")] public YoutubeSnippet Snippet { get; set; } = new();
}

public class YoutubeId
{
	[JsonPropertyName("kind")] public string? Kind { get; set; }
	[JsonPropertyName("videoId")] public string? VideoId { get; set; }
}

public class YoutubeSnippet
{
	[JsonPropertyName("title")] public string? Title { get; set; }
	[JsonPropertyName("channelTitle")] public string? ChannelTitle { get; set; }
	[JsonPropertyName("publishedAt")] public DateTime PublishedAt { get; set; }
	[JsonPropertyName("thumbnails")] public YoutubeThumbnails Thumbnails { get; set; } = new();
}

public class YoutubeThumbnails
{
	[JsonPropertyName("default")] public YoutubeThumb? Default { get; set; }
	[JsonPropertyName("medium")] public YoutubeThumb? Medium { get; set; }
	[JsonPropertyName("high")] public YoutubeThumb? High { get; set; }
}

public class YoutubeThumb
{
	[JsonPropertyName("url")] public string? Url { get; set; }
	[JsonPropertyName("width")] public int? Width { get; set; }
	[JsonPropertyName("height")] public int? Height { get; set; }
}