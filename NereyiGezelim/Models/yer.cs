namespace NereyiGezelim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("yer")]
    public partial class yer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public yer()
        {
            yorums = new HashSet<yorum>();
            etikets = new HashSet<etiket>();
        }

        public int yerid { get; set; }

        [StringLength(500)]
        public string baslik { get; set; }

        public string icerik { get; set; }

        [StringLength(500)]
        public string foto { get; set; }

        public DateTime? tarih { get; set; }

        public int? kategoriid { get; set; }

        public int? uyeid { get; set; }

        public int? okunma { get; set; }

        public int? begenme { get; set; }

        public virtual kategori kategori { get; set; }

        public virtual uye uye { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yorum> yorums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<etiket> etikets { get; set; }
    }
}
