using System;
using System.Collections.Generic;
using System.Text;

namespace DK.Domain.DTO.Base
{
    public class GeneralFileResult
    {
        /// <summary>
        /// نام فایل
        /// </summary>
        public string xFileName { get; set; }

        /// <summary>
        /// فرمت فایل
        /// </summary>
        public string xContentType { get; set; }

        /// <summary>
        /// فایل
        /// </summary>
        public byte[] xFile { get; set; }
    }
}
