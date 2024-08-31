using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class DoctorDto
    {
        public int? Id { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image {  get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public int Work_experience { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool Archived { get; set; }

        // ---- navigation properties
        //public Category? Category { get; set; }
    }
}
