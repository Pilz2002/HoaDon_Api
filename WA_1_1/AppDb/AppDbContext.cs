using Microsoft.EntityFrameworkCore;
using WA_1_1.Entities;

namespace WA_1_1.AppDb
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = QUYEN; Database = WA_1_1; Trusted_Connection = True;" +
                "TrustServerCertificate=True");
        }
    }
}
