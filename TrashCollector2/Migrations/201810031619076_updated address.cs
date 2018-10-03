namespace TrashCollector2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedaddress : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Zipcode", c => c.String());
            AlterColumn("dbo.Employees", "ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "ZipCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Addresses", "Zipcode", c => c.Int(nullable: false));
        }
    }
}
