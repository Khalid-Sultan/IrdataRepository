namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Volunteer2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VolunteeringEvents", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VolunteeringEvents", "Description", c => c.Int(nullable: false));
        }
    }
}
