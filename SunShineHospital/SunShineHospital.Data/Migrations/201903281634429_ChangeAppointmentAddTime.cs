namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAppointmentAddTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appoinments", "Time", c => c.String());
            AddColumn("dbo.Appoinments", "Day", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appoinments", "Day");
            DropColumn("dbo.Appoinments", "Time");
        }
    }
}
