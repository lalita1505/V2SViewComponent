using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace V2SViewComponent.Models
{
    public class Employee
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public DateTime DOB { get; set; }

        public string Designation { get; set; }
        
        public string City
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
    }
}
