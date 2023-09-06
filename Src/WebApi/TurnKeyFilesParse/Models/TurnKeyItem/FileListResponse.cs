using System.Collections.Generic;

namespace Api.Models
{
    public class FileListResponse
    {
        public FileListResponse(object master)
        {
            this.Master = master;
        }
        public object Master { get; set; }

        public List<object> Details { get; set; }
    }

    public class E0501Master
    {

        public E0501Master(string fromPartyId,
            string fromVACRoutingId,
            string toPartyId,
            string toVACRoutingId,
            string fileVerion,
            string fileApplyBusinessID,
            string fileDate,
            string fileSequence
            )
        {
            this.FileVersion = fileVerion;
            this.FileApplyBusinessID = fileApplyBusinessID;
            this.FileDate = fileDate;
            this.FileSequence = fileSequence;
            this.FromPartyId = fromPartyId;
            this.FromVACRoutingId = fromVACRoutingId;
            this.ToPartyId = toPartyId;
            this.ToVACRoutingId = toVACRoutingId;
        }

        public string FileVersion { get; set; } = string.Empty;
        public string FileApplyBusinessID { get; set; } = string.Empty;
        public string FileDate { get; set; } = string.Empty;
        public string FileSequence { get; set; } = string.Empty;

        public string FromPartyId { get; set; }
        public string FromVACRoutingId { get; set; }
        public string ToPartyId { get; set; }
        public string ToVACRoutingId { get; set; }
    }

    public class E0501Detail
    {
        public E0501Detail(string sellerId, string trackID,
            string yearMonth, string startNo, string endNo,
            int booklet)
        {
            this.SellerId = sellerId;
            this.TrackID = trackID;
            this.YearMonth = yearMonth;
            this.StartNo = startNo;
            this.EndNo = endNo;
            this.Booklet = booklet;
        }

        public string SellerId { get; set; }

        public string TrackID { get; set; }

        public string YearMonth { get; set; }

        public string StartNo { get; set; }

        public string EndNo { get; set; }
        public int Booklet { get; set; }

    }
}