namespace SchoolManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stage3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TeacherId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TeacherId");
        }
    }
}
