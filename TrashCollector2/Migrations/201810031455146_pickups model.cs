namespace TrashCollector2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pickupsmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PickUps",
                c => new
                    {
                        PickUpId = c.Int(nullable: false, identity: true),
                        PickCustomerId = c.Int(nullable: false),
                        DayOfWeek = c.String(),
                        PickUpDate = c.DateTime(),
                        Cost = c.Double(nullable: false),
                        Zipcode = c.String(),
                        SuspendPickUpStart = c.DateTime(),
                        SuspendPickUpEnd = c.DateTime(),
                    })
                .PrimaryKey(t => t.PickUpId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PickUps");
        }
    }
}
