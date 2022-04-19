using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppModel
{
   public  class Product
    //all the items are database column name
    {
        [Key]
        public int Id { get; set; }
        [Required]        
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        public string imageurl { get; set; }
       
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category category { get; set; }//navigation porperty always makes the id intoforegin key



    }
}
