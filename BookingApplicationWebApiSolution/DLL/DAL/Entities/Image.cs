using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DLL.DAL.Entities
{
    [Serializable]
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public int ImageSize { get; set; }
        public byte[] ImageData { get; set; }

        [Required(ErrorMessage="Please select Image file")]
        public HttpPostedFileBase File { get; set; }

    }
}
