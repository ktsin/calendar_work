using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EFCore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTask> ProjectTasks { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region MessaagesConfig

            modelBuilder.Entity<Message>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.Sender);
            modelBuilder.Entity<Message>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.Recipient);

            #endregion

            #region CalendarEventsConfig

            modelBuilder.Entity<CalendarEvent>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
            // modelBuilder.Entity<CalendarEvent>()
            //     .HasMany(e => e.Tags)
            //     .WithMany(e => e.CalendarEvents)
            //     .UsingEntity(e => e.ToTable("EventTag"));

            #endregion

            #region GroupsConfig

            modelBuilder.Entity<Group>()
                .HasMany<User>(e => e.GroupParticipants)
                .WithMany(e => e.Groups)
                .UsingEntity(e => e.ToTable("GroupUser"));
            modelBuilder.Entity<Group>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.CommandOwner);

            #endregion

            #region ProjectsConfig

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Participants)
                .WithMany(e => e.InProjects)
                .UsingEntity(e => e.ToTable("ProjectUser"));
            modelBuilder.Entity<Project>()
                .HasOne<User>()
                .WithMany(e => e.UserProjects)
                .HasForeignKey(e => e.ProjectOwner)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Project>()
                .HasMany<ProjectTask>(e => e.Tasks)
                .WithOne()
                .HasForeignKey(e => e.ProjectId);

            #endregion

            #region ProjectTaskConfig

            modelBuilder.Entity<ProjectTask>()
                .HasMany(e => e.Participants)
                .WithMany(e => e.Tasks)
                .UsingEntity(e => e.ToTable("ProjectTaskUser"));
            modelBuilder.Entity<ProjectTask>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.ProjectTasks)
                .UsingEntity(e => e.ToTable("TagProjectTask"));
            modelBuilder.Entity<ProjectTask>()
                .HasOne<Project>()
                .WithMany(e => e.Tasks)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region TagsConfig

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.ProjectTasks)
                .WithMany(e => e.Tags)
                .UsingEntity(e => e.ToTable("ProjectTaskTag"));
            modelBuilder.Entity<Tag>()
                .HasMany(e => e.CalendarEvents)
                .WithMany(e => e.Tags)
                .UsingEntity(e => e.ToTable("CalendarEventTag"));
            modelBuilder.Entity<Tag>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.OwnerId);

            #endregion

            #region UsersConfig

            modelBuilder.Entity<User>()
                .HasMany(e => e.Groups)
                .WithMany(e => e.GroupParticipants)
                .UsingEntity(e => e.ToTable("UserGroup"));
            modelBuilder.Entity<User>()
                .HasMany(e => e.Tasks)
                .WithMany(e => e.Participants)
                .UsingEntity(e => e.ToTable("UserProjectTask"));
            modelBuilder.Entity<User>()
                .HasMany(e => e.InProjects)
                .WithMany(e => e.Participants)
                .UsingEntity(e => e.ToTable("UserProject"));
            modelBuilder.Entity<User>()
                .HasMany(e => e.UserProjects)
                .WithOne()
                .HasForeignKey(e => e.ProjectOwner);

            #endregion
        }
    }
}