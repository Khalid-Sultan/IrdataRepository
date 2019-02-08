namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FundMes", "status", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.FundMes", "status");
        }
    }
}
