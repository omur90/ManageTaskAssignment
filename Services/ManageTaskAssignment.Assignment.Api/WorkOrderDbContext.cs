using ManageTaskAssignment.Assignment.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageTaskAssignment.Assignment.Api
{
    public class WorkOrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "workOrder";

        public WorkOrderDbContext(DbContextOptions<WorkOrderDbContext> options) : base(options)
        {
        }

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderDetail> WorkOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkOrder>().ToTable("tblWorkOrders", DEFAULT_SCHEMA);
            modelBuilder.Entity<WorkOrderDetail>().ToTable("tblWorkOrderDetails", DEFAULT_SCHEMA);

            modelBuilder.Entity<WorkOrder>().Property(x => x.Id).HasDefaultValue(Guid.NewGuid());

            modelBuilder.Entity<WorkOrderDetail>().Property(x => x.Id).HasDefaultValue(Guid.NewGuid());

            base.OnModelCreating(modelBuilder);
        }
    }
}
