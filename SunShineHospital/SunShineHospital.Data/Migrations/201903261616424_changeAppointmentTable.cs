namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAppointmentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appoinments", "PhoneNumber", c => c.String());
            AddColumn("dbo.Appoinments", "Email", c => c.String());
            AlterColumn("dbo.Appoinments", "Healthinsurance", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appoinments", "Healthinsurance", c => c.Boolean(nullable: false));
            DropColumn("dbo.Appoinments", "Email");
            DropColumn("dbo.Appoinments", "PhoneNumber");
        }
    }
}
