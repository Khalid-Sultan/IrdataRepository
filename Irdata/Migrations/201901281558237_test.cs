namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FundMes", "ApplicationUser_Id", "dbo.AspNetUsers");
            AddColumn("dbo.AspNetUsers", "FundMe_FundMeId", c => c.Int());
            AddColumn("dbo.FundMes", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "FundMe_FundMeId");
            CreateIndex("dbo.FundMes", "ApplicationUser_Id1");
            AddForeignKey("dbo.AspNetUsers", "FundMe_FundMeId", "dbo.FundMes", "FundMeId");
            AddForeignKey("dbo.FundMes", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FundMes", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "FundMe_FundMeId", "dbo.FundMes");
            DropIndex("dbo.FundMes", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "FundMe_FundMeId" });
            DropColumn("dbo.FundMes", "ApplicationUser_Id1");
            DropColumn("dbo.AspNetUsers", "FundMe_FundMeId");
            AddForeignKey("dbo.FundMes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
