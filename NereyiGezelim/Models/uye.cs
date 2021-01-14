namespace NereyiGezelim.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uye")]
    public partial class uye
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public uye()
        {
            yers = new HashSet<yer>();
            yorums = new HashSet<yorum>();
        }

        public int uyeid { get; set; }

        [StringLength(50)]
        public string kullaniciadi { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(20)]
        public string sifre { get; set; }

        [StringLength(50)]
        public string adsoyad { get; set; }

        [StringLength(250)]
        public string foto { get; set; }

        public int? yetkiid { get; set; }

        public virtual yetki yetki { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yer> yers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yorum> yorums { get; set; }
    }
}
