using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Partner.Comms.DTO
{
    public class DocketHashDTO : TableEntity
    {
        public DocketHashDTO(string docket_id, string docket_hash_value)
        {
            DocketHashValue = docket_hash_value;
            PartitionKey = "hash"; 
            RowKey = docket_id;
        }
        public string DocketHashValue { get; set; }


    }
}
