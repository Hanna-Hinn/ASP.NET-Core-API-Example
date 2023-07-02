using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API_Project.Entities
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string details { get; set; }
        public double price { get; set; }
        public DateTime IssueDate { get; set; }

        public int CustomerId { get; set; }
        public Customers Customer { get; set; }
 

      

    }
}
