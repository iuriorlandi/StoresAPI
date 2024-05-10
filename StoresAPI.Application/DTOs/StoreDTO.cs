using StoresAPI.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoresAPI.Application.DTOs
{
    public class StoreDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = $"The {nameof(StoreDTO.CompanyId)} must be greater than 0")]
        public int CompanyId { get; set; }

        public Store ToStore()
        {
            return new Store
            {
                CompanyId = CompanyId,
                Country = Country,
                Address = Address,
                Name = Name
            };
        }
    }
}
