namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test101 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funders",
                c => new
                    {
                        FunderId = c.Int(nullable: false, identity: true),
                        ApplicationUsersAndPledge = c.String(),
                        FundMe_FundMeId = c.Int(),
                    })
                .PrimaryKey(t => t.FunderId)
                .ForeignKey("dbo.FundMes", t => t.FundMe_FundMeId)
                .Index(t => t.FundMe_FundMeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Funders", "FundMe_FundMeId", "dbo.FundMes");
            DropIndex("dbo.Funders", new[] { "FundMe_FundMeId" });
            DropTable("dbo.Funders");
        }
    }
}
