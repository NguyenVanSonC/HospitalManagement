namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCommentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropPrimaryKey("dbo.Comments");
            AddColumn("dbo.Comments", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Comments", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Comments", "ID");
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.ApplicationUsers", "Id");
            DropColumn("dbo.Comments", "ParentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "ParentID", c => c.Int());
            DropForeignKey("dbo.Comments", "UserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.Comments", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Comments", "ID");
            AddPrimaryKey("dbo.Comments", new[] { "DoctorID", "UserId" });
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
        }
    }
}
