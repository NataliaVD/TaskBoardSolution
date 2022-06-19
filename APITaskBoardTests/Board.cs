using System.Text.Json.Serialization;

namespace APITaskBoardTests
{
    public class Board
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        public Board(string name)
        {
            this.name = name;
        }
        
    }
}