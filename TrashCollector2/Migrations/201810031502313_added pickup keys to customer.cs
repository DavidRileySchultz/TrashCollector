namespace TrashCollector2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpickupkeystocustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PickId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "UserName", c => c.String());
            AddColumn("dbo.Customers", "AccountBalance", c => c.Double(nullable: false));
            CreateIndex("dbo.Customers", "PickId");
            AddForeignKey("dbo.Customers", "PickId", "dbo.PickUps", "PickUpId", cascadeDelete: true);
            DropColumn("dbo.Customers", "PickUpDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "PickUpDay", c => c.String());
            DropForeignKey("dbo.Customers", "PickId", "dbo.PickUps");
            DropIndex("dbo.Customers", new[] { "PickId" });
            DropColumn("dbo.Customers", "AccountBalance");
            DropColumn("dbo.Customers", "UserName");
            DropColumn("dbo.Customers", "PickId");
        }
    }
}
