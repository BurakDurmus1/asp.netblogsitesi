namespace NereyiGezelim.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class geziDB : DbContext
    {
        public geziDB()
            : base("name=geziDB")
        {
        }

        public virtual DbSet<etiket> etikets { get; set; }
        public virtual DbSet<kategori> kategoris { get; set; }
        public virtual DbSet<uye> uyes { get; set; }
        public virtual DbSet<yer> yers { get; set; }
        public virtual DbSet<yetki> yetkis { get; set; }
        public virtual DbSet<yorum> yorums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<etiket>()
                .HasMany(e => e.yers)
                .WithMany(e => e.etikets)
                .Map(m => m.ToTable("yeretiket").MapLeftKey("etiketid").MapRightKey("yerid"));
        }
    }
}
