﻿namespace Memorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReligionTypesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReligionTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReligionTypes");
        }
    }
}
