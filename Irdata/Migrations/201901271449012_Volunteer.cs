namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Volunteer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VolunteeringEvents",
                c => new
                    {
                        VolunteeringEventsId = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        Title = c.String(),
                        Organization = c.String(),
                        Description = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VolunteeringEventsId);
            
            CreateTable(
                "dbo.VolunteerFiles",
                c => new
                    {
                        VolunteerFileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        VolunteeringEvents_VolunteeringEventsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VolunteerFileId)
                .ForeignKey("dbo.VolunteeringEvents", t => t.VolunteeringEvents_VolunteeringEventsId, cascadeDelete: true)
                .Index(t => t.VolunteeringEvents_VolunteeringEventsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VolunteerFiles", "VolunteeringEvents_VolunteeringEventsId", "dbo.VolunteeringEvents");
            DropIndex("dbo.VolunteerFiles", new[] { "VolunteeringEvents_VolunteeringEventsId" });
            DropTable("dbo.VolunteerFiles");
            DropTable("dbo.VolunteeringEvents");
        }
    }
}
