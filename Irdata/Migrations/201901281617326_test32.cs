namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test32 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FundMeApplicationUsers", "FundMe_FundMeId", "dbo.FundMes");
            DropForeignKey("dbo.FundMeApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FundMeApplicationUsers", new[] { "FundMe_FundMeId" });
            DropIndex("dbo.FundMeApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.FundMes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.FundMes", "ApplicationUser_Id");
            AddForeignKey("dbo.FundMes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.FundMeApplicationUsers");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.FundMeApplicationUsers",
                c => new
                {
                    FundMe_FundMeId = c.Int(nullable: false),
                    ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.FundMe_FundMeId, t.ApplicationUser_Id });

            DropForeignKey("dbo.FundMes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FundMes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.FundMes", "ApplicationUser_Id");
            CreateIndex("dbo.FundMeApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.FundMeApplicationUsers", "FundMe_FundMeId");
            AddForeignKey("dbo.FundMeApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FundMeApplicationUsers", "FundMe_FundMeId", "dbo.FundMes", "FundMeId", cascadeDelete: true);
        }
    }
}
