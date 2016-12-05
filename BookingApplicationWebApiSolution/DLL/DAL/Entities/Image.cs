using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;


namespace DLL.DAL.Entities
{

    public class Image
    {
        public int ImageId { get; set; }

        public string ImageFileName { get; set; }
        public string ImageMimeType { get; set; }
        [DisplayName("Billede")]
        [MaxLength]
        public byte[] PhotoFile { get; set; }
    }
}
