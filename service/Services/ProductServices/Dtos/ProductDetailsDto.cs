using dataa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.ProductServices.Dtos
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public string PicURL { get; set; }
     
        public string BrandName{ get; set; }
       
        public string TypeName { get; set; }

    }

}
