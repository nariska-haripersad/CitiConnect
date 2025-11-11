using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiConnect
{
    // A class for searvice requests
    public class ServiceRequestNode
    {
        public int Id;
        public string Name;
        public string Description;
        public string Category;
        public string Location;
        public string Status;
        public DateTime RequestDate;
        public ServiceRequestNode Left;
        public ServiceRequestNode Right;

        public ServiceRequestNode(int id, string name, string description, string category, string location, string status, DateTime requestDate)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            Location = location;
            Status = status;
            RequestDate = requestDate;
            Left = Right = null;
        }
    }
}
