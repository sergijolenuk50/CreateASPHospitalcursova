using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFileServices
    {
        Task<string> SaveDoctorImage(IFormFile file);
        Task DeleteDoctorImage(string path);
        Task<string> EditDoctorImage(string oldPath, IFormFile newFile);
    }
}
