using MicroService.Models;
using MicroService.Models.CustomModule;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Repository
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
            #region 自定义模块
            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("tb_CustomModule");
                entity.HasMany(w => w.CustomPropertys).WithOne(w => w.CustomModule).HasPrincipalKey(w=>w.Id).HasForeignKey(w => w.ModuleId);
            });
            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("tb_CustomProperty");
            });
            modelBuilder.Entity<PropertyValue>(entity =>
            {
                entity.ToTable("tb_CustomPropertyValue");
            });
            #endregion
            #region Chat
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("TbChatMessage");
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
