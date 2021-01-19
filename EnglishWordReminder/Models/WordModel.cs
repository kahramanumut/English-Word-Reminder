using System.Text.Json.Serialization;

namespace EnglishWordReminder.Models
{
    public class WordModel
    {
        [JsonPropertyName("turkish")]
        public string Turkish { get; set; }
        
        [JsonPropertyName("english")]
        public string English { get; set; }
    }
}
