﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SapunovProjectDB.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdressClient> AdressClient { get; set; }
        public virtual DbSet<AdressStaff> AdressStaff { get; set; }
        public virtual DbSet<CategoryOfService> CategoryOfService { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<GenderStaff> GenderStaff { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<PassportClient> PassportClient { get; set; }
        public virtual DbSet<PassportStaff> PassportStaff { get; set; }
        public virtual DbSet<PositionAtWork> PositionAtWork { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StatusOrder> StatusOrder { get; set; }
        public virtual DbSet<TypeOfWork> TypeOfWork { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
