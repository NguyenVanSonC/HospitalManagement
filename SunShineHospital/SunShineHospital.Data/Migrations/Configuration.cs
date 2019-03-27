using System.Collections.Generic;

namespace SunShineHospital.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SunShineHospital.Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SunShineHospital.Data.SunShineHospitalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SunShineHospital.Data.SunShineHospitalDbContext context)
        {
           CreateDoctor(context);
        }

        private void CreateDoctor(SunShineHospital.Data.SunShineHospitalDbContext context)
        {
            if (context.Doctors.Count() == 0)
            {
                List<Doctor> listDoctors = new List<Doctor>()
                {
                    new Doctor()
                    {
                        Alias = "a",
                        DepartmentID = 13,
                        Image = "Assets/Client/img/doctor.jpg",
                        Salary = 7000000,
                        Education = "Đại học y Hà Nội",
                        HomeFlag = true,
                        HotFlag = true,
                        ViewCount = 0,
                        Status = true,
                        CreatedDate = DateTime.Now,
                        UserId = "0de9a06d-f83a-44ce-9194-774bdb52c0ff"
                    },
                    new Doctor()
                    {
                        Alias = "a",
                        DepartmentID = 13,
                        Image = "Assets/Client/img/doctor.jpg",
                        Salary = 7000000,
                        Education = "Đại học y Hà Nội",
                        HomeFlag = true,
                        HotFlag = true,
                        ViewCount = 0,
                        Status = true,
                        CreatedDate = DateTime.Now,
                        UserId = "8601d058-b9ad-4b1f-acbf-2cc8f01942fd"
                    },
                    new Doctor()
                    {
                        Alias = "a",
                        DepartmentID = 13,
                        Image = "Assets/Client/img/doctor.jpg",
                        Salary = 7000000,
                        Education = "Đại học y Hà Nội",
                        HomeFlag = true,
                        HotFlag = true,
                        ViewCount = 0,
                        Status = true,
                        CreatedDate = DateTime.Now,
                        UserId = "5161dd59-330f-4b83-9c8a-6a3f77c9d253"
                    },
                    new Doctor()
                    {
                        Alias = "a",
                        DepartmentID = 13,
                        Image = "Assets/Client/img/doctor.jpg",
                        Salary = 7000000,
                        Education = "Đại học y Hà Nội",
                        HomeFlag = true,
                        HotFlag = true,
                        ViewCount = 0,
                        Status = true,
                        CreatedDate = DateTime.Now,
                        UserId = "8c08a280-45ee-47fa-b26f-7346d4b34274"
                    },
                    new Doctor()
                    {
                        Alias = "a",
                        DepartmentID = 13,
                        Image = "Assets/Client/img/doctor.jpg",
                        Salary = 7000000,
                        Education = "Đại học y Hà Nội",
                        HomeFlag = true,
                        HotFlag = true,
                        ViewCount = 0,
                        Status = true,
                        CreatedDate = DateTime.Now,
                        UserId = "b213c150-2f72-4ed2-bf9b-bb9baeb938ff"
                    },
                    new Doctor()
                    {
                        Alias = "a",
                        DepartmentID = 13,
                        Image = "Assets/Client/img/doctor.jpg",
                        Salary = 7000000,
                        Education = "Đại học y Hà Nội",
                        HomeFlag = true,
                        HotFlag = true,
                        ViewCount = 0,
                        Status = true,
                        CreatedDate = DateTime.Now,
                        UserId = "f979e50b-002d-4f20-888d-0f68f43774ca"
                    }
                };
                context.Doctors.AddRange(listDoctors);
                context.SaveChanges();
            }
        }
        private void CreateUser(SunShineHospital.Data.SunShineHospitalDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SunShineHospitalDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new SunShineHospitalDbContext()));
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Patient" });
                roleManager.Create(new IdentityRole { Name = "Doctor" });
                roleManager.Create(new IdentityRole { Name = "Director" });
                roleManager.Create(new IdentityRole { Name = "Dean" });
                roleManager.Create(new IdentityRole { Name = "Deputy-Dean"});
                roleManager.Create(new IdentityRole { Name = "Nurse" });
            }

            var listUser = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName = "Doctor1",
                    Email = "doctor1.sunshine@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Nguyễn Bình An",
                    PhoneNumber = "(350)476-3683",
                    Gender = true
                },
                new ApplicationUser()
                {
                    UserName = "Doctor2",
                    Email = "doctor2.sunshine@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Nguyễn Đức Minh",
                    PhoneNumber = "(224)385-0916",
                    Gender = true

                },
                new ApplicationUser()
                {
                    UserName = "Doctor3",
                    Email = "doctor3.sunshine@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Nguyễn Gia Nguyên",
                    PhoneNumber = "(802)984-0909",
                    Gender = true

                },
                new ApplicationUser()
                {
                    UserName = "Doctor4",
                    Email = "doctor4.sunshine@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Nguyễn Mai Anh",
                    PhoneNumber = "(656)379-2683",
                    Gender = false

                },
                new ApplicationUser()
                {
                    UserName = "Doctor5",
                    Email = "doctor5.sunshine@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Nguyễn Diệp Chi:",
                    PhoneNumber = "(256)964-3885",
                    Gender = false

                },
                new ApplicationUser()
                {
                    UserName = "Doctor6",
                    Email = "doctor6.sunshine@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Nguyễn Trúc Quỳnh",
                    PhoneNumber = "(543)985-2998",
                    Gender = false

                }
            };
            foreach (var user in listUser)
            {
                manager.Create(user, "123456");
                manager.AddToRoles(user.Id, new string[] { "Doctor", "Patient" });
            }
        }

        private void CreateDeparment(SunShineHospital.Data.SunShineHospitalDbContext context)
        {
            if (context.Departments.Count() == 0)
            {
                List<Department> listDepartments = new List<Department>()
                {
                    new Department()
                    {
                        Name = "Khoa Nhi",
                        Alias = "khoa-nhi",
                        Image = "~/Assets/Client/img/icon_cat_2.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa xét nghiệm",
                        Alias = "khoa-xet-nghiem",
                        Image = "~/Assets/Client/img/icon_cat_3.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa Răng hàm mặt",
                        Alias = "khoa-rang-ham-mat",
                        Image = "~/Assets/Client/img/icon_cat_5.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa Huyết học",
                        Alias = "khoa-huyet-hoc",
                        Image = "~/Assets/Client/img/icon_cat_4.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa Tim mạch",
                        Alias = "khoa-tim-mach",
                        Image = "~/Assets/Client/img/icon_cat_2.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa thần kinh",
                        Alias = "khoa-than-kinh",
                        Image = "~/Assets/Client/img/icon_cat_8.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa Sản",
                        Alias = "khoa-san",
                        Image = "~/Assets/Client/img/icon_cat_1.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa hô hấp",
                        Alias = "khoa-ho-hap",
                        Image = "~/Assets/Client/img/icon_cat_2.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa tai mũi họng",
                        Alias = "khoa-tai-mui-hong",
                        Image = "~/Assets/Client/img/icon_cat_8.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa xương khớp",
                        Alias = "khoa-xuong-khop",
                        Image = "~/Assets/Client/img/icon_cat_6.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa tiêu hóa",
                        Alias = "khoa-tieu-hoa",
                        Image = "~/Assets/Client/img/icon_cat_3.svg",
                        CreatedDate = DateTime.Now
                    },
                    new Department()
                    {
                        Name = "Khoa ngoại",
                        Alias = "khoa-ngoai",
                        Image = "~/Assets/Client/img/icon_cat_1.svg",
                        CreatedDate = DateTime.Now
                    }
                };
                context.Departments.AddRange(listDepartments);
                context.SaveChanges();
            }
        }
    }
}
