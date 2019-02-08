namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class z : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VolunteerDetails",
                c => new
                    {
                        VolunteerDetailsId = c.Int(nullable: false, identity: true),
                        ApplicationUsersAndLikes = c.String(),
                        VolunteeringEvents_VolunteeringEventsId = c.Int(),
                    })
                .PrimaryKey(t => t.VolunteerDetailsId)
                .ForeignKey("dbo.VolunteeringEvents", t => t.VolunteeringEvents_VolunteeringEventsId)
                .Index(t => t.VolunteeringEvents_VolunteeringEventsId);
            
            AddColumn("dbo.VolunteeringEvents", "Likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VolunteerDetails", "VolunteeringEvents_VolunteeringEventsId", "dbo.VolunteeringEvents");
            DropIndex("dbo.VolunteerDetails", new[] { "VolunteeringEvents_VolunteeringEventsId" });
            DropColumn("dbo.VolunteeringEvents", "Likes");
            DropTable("dbo.VolunteerDetails");
        }
    }
}
