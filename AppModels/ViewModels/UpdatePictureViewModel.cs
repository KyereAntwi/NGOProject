using Microsoft.AspNetCore.Http;
using System;

namespace AppModels.ViewModels
{
    public class UpdatePictureViewModel
    {
        public Guid Id { get; set; }
        public IFormFile Photograph { get; set; }
    }
}
