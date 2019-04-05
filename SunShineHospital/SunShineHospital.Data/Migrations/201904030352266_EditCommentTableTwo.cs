namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCommentTableTwo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ParentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "ParentID");
        }
    }
}
