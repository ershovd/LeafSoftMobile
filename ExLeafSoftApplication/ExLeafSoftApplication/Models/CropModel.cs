using ExLeafSoftApplication.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExLeafSoftApplication.Models
{
    public class CropModel : Observable
    {
        [PrimaryKey]
        public int Crop_ID { get; set; }
        public string Crop_Name { get; set; }
        public string Crop_Code { get; set; }
        public string Crop_LatinName { get; set; }
        public bool Crop_IsActive { get; set; }
        public string Crop_CreationDate { get; set; }
        public string Crop_UpdateDate { get; set; }
        public int Crop_CreatorId { get; set; }
        public int Crop_UpdaterId { get; set; }
    }

   
}
