using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace domain
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Product")]
        public string Product { get; set; }

        [BsonElement("PostalCode")]
        public string PostalCode { get; set; }

        [BsonElement("Address")]
        public Address Address { get; set; }

        [BsonElement("Delivered")]
        public bool Delivered { get; set; }
    }

    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Street")]
        public string Street { get; set; }
    }

    public class Postal
    {
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
    }
}
