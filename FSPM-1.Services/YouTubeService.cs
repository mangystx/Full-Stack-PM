using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace FSPM_1.Services;

public class YouTubeService(HttpClient http, IConfiguration cfg)
{
	private readonly string _apiKey = cfg["YouTube:ApiKey"] ?? string.Empty;

	private readonly JsonSerializerOptions _json = new(JsonSerializerDefaults.Web)
	{
		PropertyNameCaseInsensitive = true
	};

	public async Task<YoutubeSearchResponse> SearchAsync(string query, int maxResults = 10)
	{
		if (string.IsNullOrWhiteSpace(_apiKey))
			throw new InvalidOperationException("YouTube:ApiKey не встановлено (user-secrets або env).");

		if (maxResults is < 1 or > 50) maxResults = 10;

		var url =
			$"https://www.googleapis.com/youtube/v3/search?part=snippet&type=video&maxResults={maxResults}&q={Uri.EscapeDataString(query)}&key={Uri.EscapeDataString(_apiKey)}";

		using var resp = await http.GetAsync(url);
		resp.EnsureSuccessStatusCode();

		await using var stream = await resp.Content.ReadAsStreamAsync();
		return await JsonSerializer.DeserializeAsync<YoutubeSearchResponse>(stream, _json)
		       ?? new YoutubeSearchResponse();
	}
}