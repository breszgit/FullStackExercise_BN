using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend.Models.DB
{
    public partial class FSEXContext : DbContext
    {
        public FSEXContext()
        {
        }

        public FSEXContext(DbContextOptions<FSEXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DatTask> DatTasks { get; set; }
        public virtual DbSet<MstTaskType> MstTaskTypes { get; set; }
        public virtual DbSet<SysRole> SysRoles { get; set; }
        public virtual DbSet<SysToken> SysTokens { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=FSEX_DB;user=root;password=fse@2022", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.8.3-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("tis620_thai_ci")
                .HasCharSet("tis620");

            modelBuilder.Entity<DatTask>(entity =>
            {
                entity.HasKey(e => e.DtkId)
                    .HasName("PRIMARY");

                entity.ToTable("DAT_TASK");

                entity.HasIndex(e => e.DtkAssignToUid, "DTK_ASSIGN_TO_UID");

                entity.HasIndex(e => e.DtkCreateByUid, "DTK_CREATE_BY_UID");

                // entity.HasIndex(e => e.DtkTktId, "DTK_TKT_ID");

                entity.Property(e => e.DtkId)
                    .HasColumnType("int(11)")
                    .HasColumnName("DTK_ID");

                entity.Property(e => e.DtkAssignToUid)
                    .HasMaxLength(32)
                    .HasColumnName("DTK_ASSIGN_TO_UID");

                entity.Property(e => e.DtkComplete)
                    .HasColumnType("bit(1)")
                    .HasColumnName("DTK_COMPLETE");

                entity.Property(e => e.DtkCreateByUid)
                    .HasMaxLength(32)
                    .HasColumnName("DTK_CREATE_BY_UID");

                entity.Property(e => e.DtkCreatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("DTK_CREATEDATE");

                entity.Property(e => e.DtkDescription)
                    .HasMaxLength(250)
                    .HasColumnName("DTK_DESCRIPTION");

                entity.Property(e => e.DtkDuration)
                    .HasColumnType("int(11)")
                    .HasColumnName("DTK_DURATION");

                entity.Property(e => e.DtkDurationType)
                    .HasColumnType("enum('NULL','HOUR','DAY')")
                    .HasColumnName("DTK_DURATION_TYPE");

                entity.Property(e => e.DtkPriority)
                    .HasColumnType("int(11)")
                    .HasColumnName("DTK_PRIORITY");

                entity.Property(e => e.DtkStatus)
                    .HasColumnType("enum('PREVIEW','ASSIGNED','PROCESSING','COMPLETE')")
                    .HasColumnName("DTK_STATUS");

                entity.Property(e => e.DtkTaskName)
                    .HasMaxLength(100)
                    .HasColumnName("DTK_TASK_NAME");

                entity.Property(e => e.DtkTktId)
                    .HasColumnType("int(11)")
                    .HasColumnName("DTK_TKT_ID");

                // entity.HasOne(d => d.DtkAssignToU)
                //     .WithMany(p => p.DatTaskDtkAssignToUs)
                //     .HasForeignKey(d => d.DtkAssignToUid)
                //     .HasConstraintName("DAT_TASK_ibfk_2");

                // entity.HasOne(d => d.DtkCreateByU)
                //     .WithMany(p => p.DatTaskDtkCreateByUs)
                //     .HasForeignKey(d => d.DtkCreateByUid)
                //     .HasConstraintName("DAT_TASK_ibfk_1");

                // entity.HasOne(d => d.DtkTkt)
                //     .WithMany(p => p.DatTasks)
                //     .HasForeignKey(d => d.DtkTktId)
                //     .HasConstraintName("DAT_TASK_ibfk_3");
            });

            modelBuilder.Entity<MstTaskType>(entity =>
            {
                entity.HasKey(e => e.TktId)
                    .HasName("PRIMARY");

                entity.ToTable("MST_TASK_TYPE");

                entity.Property(e => e.TktId)
                    .HasColumnType("int(11)")
                    .HasColumnName("TKT_ID");

                entity.Property(e => e.TktCreatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("TKT_CREATEDATE");

                entity.Property(e => e.TktLastupdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TKT_LASTUPDATE");

                entity.Property(e => e.TktTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("TKT_TYPE_NAME");
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.HasKey(e => e.RlsRole)
                    .HasName("PRIMARY");

                entity.ToTable("SYS_ROLE");

                entity.Property(e => e.RlsRole)
                    .HasMaxLength(20)
                    .HasColumnName("RLS_ROLE");
            });

            modelBuilder.Entity<SysToken>(entity =>
            {
                entity.HasKey(e => e.TknUid)
                    .HasName("PRIMARY");

                entity.ToTable("SYS_TOKEN");

                entity.Property(e => e.TknUid)
                    .HasMaxLength(36)
                    .HasColumnName("TKN_UID")
                    .HasComment("UserID");

                entity.Property(e => e.TknCreatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("TKN_CREATEDATE");

                entity.Property(e => e.TknExpire)
                    .HasColumnType("datetime")
                    .HasColumnName("TKN_EXPIRE");

                entity.Property(e => e.TknToken)
                    .HasMaxLength(100)
                    .HasColumnName("TKN_TOKEN");

                // entity.HasOne(d => d.TknU)
                //     .WithOne(p => p.SysToken)
                //     .HasForeignKey<SysToken>(d => d.TknUid)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("SYS_TOKEN_ibfk_1");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.UsrUid)
                    .HasName("PRIMARY");

                entity.ToTable("SYS_USER");

                entity.HasIndex(e => e.UsrRole, "USR_ROLE");

                entity.Property(e => e.UsrUid)
                    .HasMaxLength(36)
                    .HasColumnName("USR_UID")
                    .HasComment("UserID");

                entity.Property(e => e.UsrCreatedate)
                    .HasColumnType("datetime")
                    .HasColumnName("USR_CREATEDATE");

                entity.Property(e => e.UsrFirstname)
                    .HasMaxLength(200)
                    .HasColumnName("USR_FIRSTNAME");

                entity.Property(e => e.UsrLastname)
                    .HasMaxLength(200)
                    .HasColumnName("USR_LASTNAME");

                entity.Property(e => e.UsrPwd)
                    .HasMaxLength(100)
                    .HasColumnName("USR_PWD");

                entity.Property(e => e.UsrRole)
                    .HasMaxLength(20)
                    .HasColumnName("USR_ROLE");

                entity.Property(e => e.UsrUsername)
                    .HasMaxLength(50)
                    .HasColumnName("USR_USERNAME");

                // entity.HasOne(d => d.UsrRoleNavigation)
                //     .WithMany(p => p.SysUsers)
                //     .HasForeignKey(d => d.UsrRole)
                //     .HasConstraintName("SYS_USER_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
