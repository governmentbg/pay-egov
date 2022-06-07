﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EPayments.Model.Models
{
     public partial class ObligationType
     {
        public int ObligationTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EserviceClient> EserviceClients { get; set; } = new HashSet<EserviceClient>();
     }

    public class ObligationTypeMap : EntityTypeConfiguration<ObligationType>
    {
        public ObligationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ObligationTypeId);

            this.Property(t => t.ObligationTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ObligationTypes");
            this.Property(t => t.ObligationTypeId).HasColumnName("ObligationTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
    }
