namespace NereyiGezelim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("yorum")]
    public partial class yorum
    {
        public int yorumid { get; set; }

        [StringLength(500)]
        public string icerik { get; set; }

        public int? uyeid { get; set; }

        public int? yerid { get; set; }

        public DateTime? tarih { get; set; }

        public virtual uye uye { get; set; }

        public virtual yer yer { get; set; }
    }
}
