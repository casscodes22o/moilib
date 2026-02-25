using System;
using System.Text.Json.Serialization; //displays String of enums not int index

namespace StepOne.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Bought {None, BNEW, ONLINE, PSHOP}
    public class Book
    {
        public int Id { get; set; }
        public String Title { get; set; } = String.Empty;
        public String Author { get; set; } = String.Empty;
        public Bought Purchase { get; set; } = Bought.None;
    }
}


