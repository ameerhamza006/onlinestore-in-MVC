﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onlinestore.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class shoppingEntities : DbContext
    {
        public shoppingEntities()
            : base("name=shoppingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<billing> billings { get; set; }
        public virtual DbSet<category1> category1 { get; set; }
        public virtual DbSet<category2> category2 { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<vacanci> vacancis { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<job> jobs { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<login> logins { get; set; }
        public virtual DbSet<billingg> billinggs { get; set; }
    
        public virtual ObjectResult<sp_search_Result> sp_search(string name, Nullable<int> price)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("price", price) :
                new ObjectParameter("price", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_search_Result>("sp_search", nameParameter, priceParameter);
        }
    }
}
