namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zfinale : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FundMes", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.FundMes", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.VolunteeringEvents", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.VolunteeringEvents", "Organization", c => c.String(maxLength: 100));
            AlterColumn("dbo.VolunteeringEvents", "Description", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VolunteeringEvents", "Description", c => c.String());
            AlterColumn("dbo.VolunteeringEvents", "Organization", c => c.String());
            AlterColumn("dbo.VolunteeringEvents", "Title", c => c.String());
            AlterColumn("dbo.FundMes", "Description", c => c.String());
            AlterColumn("dbo.FundMes", "Title", c => c.String());
        }
    }
}
