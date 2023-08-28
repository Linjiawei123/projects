using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace EPRPlatform.API.Repository
{
    /// <summary>
    /// 数据库数据
    /// </summary>
    public partial class DataContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext()
        {
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// 建立模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region 用户
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("tb_User");
                entity.HasMany(w => w.Rights).WithOne(w => w.User).HasPrincipalKey(w => w.Id).HasForeignKey(w => w.UserId);
                entity.HasMany(w => w.LoginLog).WithOne(w => w.User).HasPrincipalKey(w => w.Id).HasForeignKey(w => w.UserId);
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("tb_Role");
                entity.HasMany(w => w.Rights).WithOne(w => w.Role).HasPrincipalKey(w => w.Id).HasForeignKey(w => w.RoleId);
            });
            modelBuilder.Entity<RoleAndUser>(entity =>
            {
                entity.ToTable("tb_RoleAndUser");
            });
            modelBuilder.Entity<RoleAndPermission>(entity =>
            {
                entity.ToTable("tb_RolePermission");
            });
            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("tb_UserLogin");
            });
            modelBuilder.Entity<UserAndPermission>(entity =>
            {
                entity.ToTable("tb_UserPermission");
            });
            modelBuilder.Entity<UserRights>(entity =>
            {
                entity.ToView("v_UserRights");
            });
            #endregion
            #region 系统
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("tb_Menu");
            });
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("tb_Permission");
            });
            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("tb_ErrorLog");
            });
            #endregion
            #region 日志
            modelBuilder.Entity<UserLoginLog>(entity =>
            {
                entity.ToTable("tb_UserLoginLog");
                entity.HasOne(w => w.User).WithMany(w => w.LoginLog).HasPrincipalKey(w => w.Id).HasForeignKey(w => w.UserId);
            });
            modelBuilder.Entity<OperateLog>(entity =>
            {
                entity.ToTable("tb_OperateLog");
            });
            modelBuilder.Entity<OperateModule>(entity =>
            {
                entity.ToTable("tb_OperateModule");
            });
            #endregion
            #region 文件
            modelBuilder.Entity<UploadFile>(entity =>
            {
                entity.ToTable("TbUploadFile");
            });
            #endregion
            #region 部门职工
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("tb_Department");
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("tb_Employee");
            });
            modelBuilder.Entity<EmployeeContract>(entity =>
            {
                entity.ToTable("tb_EmployeeContract");
            });
            #endregion
            #region 基础管理
            modelBuilder.Entity<BSInvenType>(entity =>
            {
                entity.ToTable("BSInvenType");
                entity.HasMany(w => w.BSInvens).WithOne(w => w.BSInvenType).HasPrincipalKey(w => w.InvenTypeCode).HasForeignKey(w => w.InvenTypeCode);
            });
            modelBuilder.Entity<BSEmployee>(entity =>
            {
                entity.ToTable("BSEmployee");
            });
            modelBuilder.Entity<BSDepartment>(entity =>
            {
                entity.ToTable("BSDepartment");
                entity.HasMany(w => w.BSEmployees).WithOne(w => w.BSDepartment).HasPrincipalKey(w => w.DepartmentCode).HasForeignKey(w => w.DepartmentCode);
            });
            modelBuilder.Entity<BSCostType>(entity =>
            {
                entity.ToTable("BSCostType");
                entity.HasMany(w => w.BSCosts).WithOne(w => w.BSCostType).HasPrincipalKey(w => w.CostTypeCode).HasForeignKey(w => w.CostTypeCode);
            });
            modelBuilder.Entity<BSSupplier>(entity =>
            {
                entity.ToTable("BSSupplier");
            });
            modelBuilder.Entity<BSCustomer>(entity =>
            {
                entity.ToTable("BSCustomer");
            });
            modelBuilder.Entity<CUGrade>(entity =>
            {
                entity.ToTable("CUGrade");
            });
            modelBuilder.Entity<CUChance>(entity =>
            {
                entity.ToTable("CUChance");
            });
            modelBuilder.Entity<CUCredit>(entity =>
            {
                entity.ToTable("CUCredit");
            });
            modelBuilder.Entity<CUState>(entity =>
            {
                entity.ToTable("CUState");
            });
            modelBuilder.Entity<CUTrade>(entity =>
            {
                entity.ToTable("CUTrade");
            });
            #endregion
        }
        #region 自定义方法
        /// <summary>
        /// 软删除
        /// </summary>
        public void SoftDelete<TEntity>(TEntity entity) where TEntity : class, IDeletable
        {
            entity.IsDeleted = true;
            this.Entry(entity).Property(q => q.IsDeleted).IsModified = true;
        }
        #endregion
    }
}
