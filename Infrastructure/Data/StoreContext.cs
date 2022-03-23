
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        // 아래는 option을 가진 constructor임
        // 기본꼴에서 DbContextOptions의 타입을 Class 이름인 StoreContext로 지정 해주기. 이렇게 해야지 나중에 여러개의 DbContext가 생겨도 구별이 가능 함.
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }


        // 아래와 같이 DbSet type으로 변수 지정 하기
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        // 아래 OnModelCreating Method는 migration을 creating하게 해주는 함수 임
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}