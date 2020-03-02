using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OdataAutomapperServerQuery.Models
{
    class ProjectReportEntityConfiguration
        : IEntityTypeConfiguration<ProjectReport>
    {
        public void Configure(EntityTypeBuilder<ProjectReport> builder)
        {
            builder.HasKey(b => b.OptionId);
        }
    }
}
