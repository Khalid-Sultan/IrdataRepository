namespace Irdata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Volun : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Likes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Files", "VolunteeringEvents_VolunteeringEventsId", "dbo.VolunteeringEvents");
            DropForeignKey("dbo.Likes", "VolunteeringEvents_VolunteeringEventsId", "dbo.VolunteeringEvents");
            DropForeignKey("dbo.Files", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Files", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Files", new[] { "VolunteeringEvents_VolunteeringEventsId" });
            DropIndex("dbo.Likes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Likes", new[] { "VolunteeringEvents_VolunteeringEventsId" });
            AlterColumn("dbo.Files", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Files", "ApplicationUser_Id");
            AddForeignKey("dbo.Files", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            //DropColumn("dbo.Files", "VolunteeringEvents_VolunteeringEventsId");
            DropTable("dbo.Likes");
            //DropTable("dbo.VolunteeringEvents");
        }
        
        public override void Down()
        {
            //    CreateTable(
            //        "dbo.VolunteeringEvents",
            //        c => new
            //            {
            //                VolunteeringEventsId = c.Int(nullable: false, identity: true),
            //                Date = c.String(nullable: false),
            //                Title = c.String(nullable: false),
            //                Organization = c.String(nullable: false),
            //                Description = c.String(nullable: false),
            //            })
            //        .PrimaryKey(t => t.VolunteeringEventsId);

            //    CreateTable(
            //        "dbo.Likes",
            //        c => new
            //            {
            //                LikeId = c.Int(nullable: false, identity: true),
            //                LikeCount = c.Int(nullable: false),
            //                ApplicationUser_Id = c.String(maxLength: 128),
            //                VolunteeringEvents_VolunteeringEventsId = c.Int(),
            //            })
            //        .PrimaryKey(t => t.LikeId);

            //    AddColumn("dbo.Files", "VolunteeringEvents_VolunteeringEventsId", c => c.Int());
            //    DropForeignKey("dbo.Files", "ApplicationUser_Id", "dbo.AspNetUsers");
            //    DropIndex("dbo.Files", new[] { "ApplicationUser_Id" });
            //    AlterColumn("dbo.Files", "ApplicationUser_Id", c => c.String(maxLength: 128));
            //    CreateIndex("dbo.Likes", "VolunteeringEvents_VolunteeringEventsId");
            //    CreateIndex("dbo.Likes", "ApplicationUser_Id");
            //    CreateIndex("dbo.Files", "VolunteeringEvents_VolunteeringEventsId");
            //    CreateIndex("dbo.Files", "ApplicationUser_Id");
            //    AddForeignKey("dbo.Files", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            //    AddForeignKey("dbo.Likes", "VolunteeringEvents_VolunteeringEventsId", "dbo.VolunteeringEvents", "VolunteeringEventsId");
            //    AddForeignKey("dbo.Files", "VolunteeringEvents_VolunteeringEventsId", "dbo.VolunteeringEvents", "VolunteeringEventsId");
            //    AddForeignKey("dbo.Likes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
