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
        public string PublicId { get; set; }
        public int Bytes { get; set; }
        public string Url { get; set; }
        public string SecureUrl { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
    }
}
