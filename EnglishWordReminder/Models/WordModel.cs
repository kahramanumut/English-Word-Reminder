using System.Text.Json.Serialization;

namespace EnglishWordReminder.Models
{
    public class WordModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("word")]
        public string Word { get; set; }

        [JsonPropertyName("definition")]
        public string Definition { get; set; }

        [JsonPropertyName("example")]
        public string Example { get; set; }

        [JsonPropertyName("level")]
        public string Level { get; set; }

        [JsonPropertyName("synonyms")]
        public List<string> Synonyms { get; set; }

        [JsonPropertyName("phonetic")]
        public string Phonetic { get; set; }

        [JsonPropertyName("categoryIds")]
        public List<int> CategoryIds { get; set; }
    }
}
