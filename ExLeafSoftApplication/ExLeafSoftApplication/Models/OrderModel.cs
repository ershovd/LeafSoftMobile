using ExLeafSoftApplication.Common;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExLeafSoftApplication.Models
{
    public class CompactOrderModel : Observable
    {
        public int OrdeGuid { get; set; }
        public string OrdeFieldName { get; set; }
        public string OrdeCreationDate { get; set; }
        public int OrdeStatus { get; set; }


    }

    public class OrderModel
    {
        [PrimaryKey, AutoIncrement]
        public int Orde_Id { get; set; }
        public string Orde_LabelStickerName { get; set; }
        public string Orde_FieldGuid { get; set; }
        public string Orde_UniqueId { get; set; }
        public int Orde_StatusId { get; set; }
        public string Orde_CreationDate { get; set; }
        public string Orde_UpdateDate { get; set; }
        public int Orde_RowHash { get; set; }

    }


    public class OrderDetailModel
    {
        [PrimaryKey, AutoIncrement]
        public int Ocad_Id { get; set; }
        public string Ocad_OrderGuid { get; set; }
        public string Ocad_CropUniqueId { get; set; }
        public int Ocad_CropID { get; set; }
        public string Ocad_CropPhotoName { get; set; }

    }

}
