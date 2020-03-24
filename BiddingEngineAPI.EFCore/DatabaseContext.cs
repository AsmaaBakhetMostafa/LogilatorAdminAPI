using System;
using System.Collections.Generic;
using System.Text;
using BiddingEngineAPI.EFCore.Model;
using Microsoft.EntityFrameworkCore;

namespace BiddingEngineAPI.EFCore
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
       : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFileds { get; set; }
        public DbSet<FieldOption> FiledOptions { get; set; }

        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestDetail> RequestDetails { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<BidDetail> BidDetails { get; set; }
        public DbSet<Statu> Status { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<FixedFiled> FixedFileds { get; set; }
        public DbSet<FixedFiledDetails> FixedFiledDetails { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }


    }
}
