namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FundMes",
                c => new
                {
                    FundMeId = c.Int(nullable: false, identity: true),
                    date = c.String(),
                    Title = c.String(),
                    Description = c.String(),
                    TargetFunds = c.Int(nullable: false),
                    CurrentFunds = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.FundMeId);

            CreateTable(
                "dbo.FundMeFiles",
                c => new
                {
                    FundMeFileId = c.Int(nullable: false, identity: true),
                    FileName = c.String(maxLength: 255),
                    ContentType = c.String(maxLength: 100),
                    Content = c.Binary(),
                    FileType = c.Int(nullable: false),
                    FundMe_FundMeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.FundMeFileId)
                .ForeignKey("dbo.FundMes", t => t.FundMe_FundMeId, cascadeDelete: true)
                .Index(t => t.FundMe_FundMeId);

            CreateTable(
                "dbo.FundMeApplicationUsers",
                c => new
                {
                    FundMe_FundMeId = c.Int(nullable: false),
                    ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.FundMe_FundMeId, t.ApplicationUser_Id })
                .ForeignKey("dbo.FundMes", t => t.FundMe_FundMeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.FundMe_FundMeId)
                .Index(t => t.ApplicationUser_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.FundMeFiles", "FundMe_FundMeId", "dbo.FundMes");
            DropForeignKey("dbo.FundMeApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FundMeApplicationUsers", "FundMe_FundMeId", "dbo.FundMes");
            DropIndex("dbo.FundMeApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FundMeApplicationUsers", new[] { "FundMe_FundMeId" });
            DropIndex("dbo.FundMeFiles", new[] { "FundMe_FundMeId" });
            DropTable("dbo.FundMeApplicationUsers");
            DropTable("dbo.FundMeFiles");
            DropTable("dbo.FundMes");
        }
    }
}
