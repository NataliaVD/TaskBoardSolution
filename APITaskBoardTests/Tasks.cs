using System.Text.Json.Serialization;

namespace APITaskBoardTests
{
    public class Tasks
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("dateCreated")]
        public string dateCreated { get; set; }

        [JsonPropertyName("dateModified")]
        public string dateModified { get; set; }

        public Board board;
    }
}