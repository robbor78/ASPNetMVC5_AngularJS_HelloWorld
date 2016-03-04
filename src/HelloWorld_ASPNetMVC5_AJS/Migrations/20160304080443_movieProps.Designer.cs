using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using HelloWorld_ASPNetMVC5_AJS.Models;

namespace HelloWorld_ASPNetMVC5_AJS.Migrations
{
    [DbContext(typeof(MoviesAppContext))]
    [Migration("20160304080443_movieProps")]
    partial class movieProps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HelloWorld_ASPNetMVC5_AJS.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Director")
                        .IsRequired();

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<decimal>("TicketPrice");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");
                });
        }
    }
}
