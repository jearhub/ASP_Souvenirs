using ASP_Souvenirs.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Souvenirs.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            String adminId1 = "";
            String adminId2 = "";

            string role1 = "Admin";
            string desc1 = "This is the administrator role";

            string role2 = "Member";
            string desc2 = "This is the members role";

            string password = "P@ssw0rd";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            if (await userManager.FindByNameAsync("admin@qualitysouvenirs.co.nz") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@qualitysouvenirs.co.nz",
                    Email = "admin@qualitysouvenirs.co.nz",
                    FirstName = "Thai",
                    LastName = "Ong",
                    Address = "Building 183, 139 Carrington Road, Mt Albert, Auckland, New Zealand",
                    WorkPhoneNumber = "6902341234",
                    EmailConfirmed = true,
                    Enabled = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                adminId1 = user.Id;
            }

            //if (await userManager.FindByNameAsync("bb@bb.bb") == null)
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = "bb@bb.bb",
            //        Email = "bb@bb.bb",
            //        FirstName = "Bob",
            //        LastName = "Barker",
            //        Street = "Vermont St",
            //        City = "Surrey",
            //        Province = "BC",
            //        PostalCode = "V1P I5T",
            //        Country = "Canada",
            //        PhoneNumber = "7788951456"
            //    };

            //    var result = await userManager.CreateAsync(user);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddPasswordAsync(user, password);
            //        await userManager.AddToRoleAsync(user, role1);
            //    }
            //    adminId2 = user.Id;
            //}

            if (await userManager.FindByNameAsync("test01@mail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "test01@mail.com",
                    Email = "test01@mail.com",
                    FirstName = "test",
                    LastName = "test",
                    Address = "Auckland",
                    WorkPhoneNumber = "6572136821",
                    EmailConfirmed = true,
                    Enabled = true
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role2);
                }
            }

            //if (await userManager.FindByNameAsync("dd@dd.dd") == null)
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = "dd@dd.dd",
            //        Email = "dd@dd.dd",
            //        FirstName = "Donald",
            //        LastName = "Duck",
            //        Street = "Well St",
            //        City = "Vancouver",
            //        Province = "BC",
            //        PostalCode = "V8U R9Y",
            //        Country = "Canada",
            //        PhoneNumber = "6041234567"
            //    };

            //    var result = await userManager.CreateAsync(user);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddPasswordAsync(user, password);
            //        await userManager.AddToRoleAsync(user, role2);
            //    }
            //}
        }
    }
}
