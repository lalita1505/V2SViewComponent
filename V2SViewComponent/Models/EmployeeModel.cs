using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace V2SViewComponent.Models
{
    public class EmployeeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public string Designation { get; set; }

        public Address Address
        {
            get;
            set;
        }
    }
    public class Address
    {
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
