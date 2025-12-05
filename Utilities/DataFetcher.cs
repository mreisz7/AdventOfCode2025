namespace AdventOfCode2025.Utilities;

public class DataFetcher(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public async Task<string> FetchDataAsync(int day)
    {
        string sessionCookie = _configuration.GetValue<string>("AoCSessionCookie")
            ?? throw new ApplicationException("Session Cookie is not configured in secrets.json");
        string year = _configuration.GetValue<string>("AoCYear")
            ?? throw new ApplicationException("Year is not configured in appsettings.json");
        HttpClient client = new();
        string aocDataUrl = $"https://adventofcode.com/{year}/day/{day}/input";
        client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");

        var response = await client.GetAsync(aocDataUrl);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return $"Unable to fetch data for day {day}.";
    }
}