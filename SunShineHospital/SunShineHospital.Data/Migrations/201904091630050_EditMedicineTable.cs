namespace SunShineHospital.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMedicineTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prescriptions", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Prescriptions", "PatientID", "dbo.Patients");
            DropIndex("dbo.Prescriptions", new[] { "DoctorID" });
            DropIndex("dbo.Prescriptions", new[] { "PatientID" });
            CreateTable(
                "dbo.MedicineCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Medicines", "CategoryID", c => c.Int(nullable: false));
            AddColumn("dbo.Prescriptions", "AppointmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Medicines", "CategoryID");
            CreateIndex("dbo.Prescriptions", "AppointmentID");
            AddForeignKey("dbo.Medicines", "CategoryID", "dbo.MedicineCategories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Prescriptions", "AppointmentID", "dbo.Appoinments", "ID", cascadeDelete: true);
            DropColumn("dbo.Prescriptions", "DoctorID");
            DropColumn("dbo.Prescriptions", "PatientID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prescriptions", "PatientID", c => c.Int(nullable: false));
            AddColumn("dbo.Prescriptions", "DoctorID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Prescriptions", "AppointmentID", "dbo.Appoinments");
            DropForeignKey("dbo.Medicines", "CategoryID", "dbo.MedicineCategories");
            DropIndex("dbo.Prescriptions", new[] { "AppointmentID" });
            DropIndex("dbo.Medicines", new[] { "CategoryID" });
            DropColumn("dbo.Prescriptions", "AppointmentID");
            DropColumn("dbo.Medicines", "CategoryID");
            DropTable("dbo.MedicineCategories");
            CreateIndex("dbo.Prescriptions", "PatientID");
            CreateIndex("dbo.Prescriptions", "DoctorID");
            AddForeignKey("dbo.Prescriptions", "PatientID", "dbo.Patients", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Prescriptions", "DoctorID", "dbo.Doctors", "ID", cascadeDelete: true);
        }
    }
}
