using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TrainingAlex.Models
{
    public class MiningModel
    {
        [BsonElement("_id")]
        public int Id { get; set; }

        [BsonElement("project")]
        public string? Project { get; set; }


        public string? Company { get; set; }
        public string? Year { get; set; }

    }
}
