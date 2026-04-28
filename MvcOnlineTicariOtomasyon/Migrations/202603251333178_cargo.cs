namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cargo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CargoDetails",
                c => new
                    {
                        CargoDetailID = c.Int(nullable: false, identity: true),
                        Explanation = c.String(maxLength: 300, unicode: false),
                        TrackingCode = c.String(maxLength: 10, unicode: false),
                        Employee = c.String(maxLength: 20, unicode: false),
                        Buyer = c.String(maxLength: 25, unicode: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CargoDetailID);
            
            CreateTable(
                "dbo.CargoTrackings",
                c => new
                    {
                        CargoTrackingID = c.Int(nullable: false, identity: true),
                        TrackingCode = c.String(maxLength: 10, unicode: false),
                        Explanation = c.String(maxLength: 100, unicode: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CargoTrackingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CargoTrackings");
            DropTable("dbo.CargoDetails");
        }
    }
}
