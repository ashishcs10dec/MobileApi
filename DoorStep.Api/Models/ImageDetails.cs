using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoorStep.Api.Models
{
    public class ImageDetails
    {
            public int ImgId { get; set; }
            public byte[] ImageByte { get; set; }
            public string ImagePath { get; set; }
            public HttpPostedFileWrapper ImageFile { get; set; }
    }
}