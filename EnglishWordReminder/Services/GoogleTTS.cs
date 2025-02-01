namespace EnglishWordReminder.Services;
using System.IO;

public class GoogleTTS
{

    public async Task<MemoryStream> GetAudioStream(string word)
    {
        using var client = new HttpClient();
        var url = $"https://translate.google.com/translate_tts?ie=UTF-8&q={Uri.EscapeDataString(word)}&tl=en&client=tw-ob";
        
        try
        {
            var response = await client.GetByteArrayAsync(url);
            var memoryStream = new MemoryStream(response);
            return memoryStream;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading audio: {ex.Message}");
            return null;
        }
    }
}