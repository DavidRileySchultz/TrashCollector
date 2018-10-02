namespace TrashCollector2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedforeignkeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "AddressID", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "PickUpDay", c => c.String());
            AddColumn("dbo.Customers", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Employees", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "AddressID");
            CreateIndex("dbo.Customers", "ApplicationUserId");
            CreateIndex("dbo.Employees", "ApplicationUserId");
            AddForeignKey("dbo.Customers", "AddressID", "dbo.Addresses", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employees", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "AddressID", "dbo.Addresses");
            DropIndex("dbo.Employees", new[] { "ApplicationUserId" });
            DropIndex("dbo.Customers", new[] { "ApplicationUserId" });
            DropIndex("dbo.Customers", new[] { "AddressID" });
            DropColumn("dbo.Employees", "ApplicationUserId");
            DropColumn("dbo.Customers", "ApplicationUserId");
            DropColumn("dbo.Customers", "PickUpDay");
            DropColumn("dbo.Customers", "AddressID");
        }
    }
}
