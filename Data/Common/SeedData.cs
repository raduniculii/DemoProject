using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoProject.Data.Common
{
    internal class SeedData{
        private List<AppRole> defaultRoles = new List<AppRole>();
        private List<AppClaimType> defaultClaims = new List<AppClaimType>();
        private List<AppRoleClaim> defaultRoleClaims = new List<AppRoleClaim>();
        private List<AppUser> defaultUsers = new List<AppUser>();
        private List<AppUserRole> defaultUserRoles = new List<AppUserRole>();

        private SeedData(){}
        public static void PopulateTables(ModelBuilder modelBuilder){
            var sd = new SeedData();
            sd.Populate(modelBuilder);
        }

        private void Populate(ModelBuilder modelBuilder)
        {
            populateRoles();
            populateClaimTypes();
            populateRoleClaimsAndUpdateRolePermissions();
            populateUsers();
            populateUserRolesAndUpdateUserRole();

            modelBuilder.Entity<AppRole>().HasData(defaultRoles);
            modelBuilder.Entity<AppRole_Hist>().HasData(defaultRoles.Select(r => r.GetHistRecord())!);

            modelBuilder.Entity<AppClaimType>().HasData(defaultClaims);
            modelBuilder.Entity<AppClaimType_Hist>().HasData(defaultClaims.Select(r => r.GetHistRecord())!);

            modelBuilder.Entity<AppRoleClaim>().HasData(defaultRoleClaims);
            modelBuilder.Entity<AppRoleClaim_Hist>().HasData(defaultRoleClaims.Select(r => r.GetHistRecord())!);

            modelBuilder.Entity<AppUser>().HasData(defaultUsers);
            modelBuilder.Entity<AppUser_Hist>().HasData(defaultUsers.Select(r => r.GetHistRecord())!);

            modelBuilder.Entity<AppUserRole>().HasData(defaultUserRoles);
            modelBuilder.Entity<AppUserRole_Hist>().HasData(defaultUserRoles.Select(r => r.GetHistRecord())!);
        }

        private void populateRoles()
        {
            defaultRoles.Add(new AppRole{
                Id = 1
                , Ver = 1
                , HistActionPerformedById = 1
                , HistActionPerformedBy = "admin@demo.test"
                , HistActionType = "i"
                , HistActionDate = new DateTime(2022, 09, 26)
                , Name = "Administrator"
                , NormalizedName = "ADMINISTRATOR"
                , Description = "Full sys admin"
                , Permissions = ""
            });

            for(var i = 2; i <= 273; i++){
                defaultRoles.Add(new AppRole{
                    Id = i
                    , Ver = 1
                    , HistActionPerformedById = 1
                    , HistActionPerformedBy = "admin@demo.test"
                    , HistActionType = "i"
                    , HistActionDate = new DateTime(2022, 09, 26)
                    , Name = $"Role {i.ToString("D3")}"
                    , NormalizedName = $"ROLE {i.ToString("D3")}"
                    , Description = ""
                    , Permissions = ""
                });
            }
        }

        private void populateClaimTypes()
        {
            defaultClaims.Add(new AppClaimType{
                Id = 1
                , Ver = 1
                , HistActionPerformedById = 1
                , HistActionPerformedBy = "admin@demo.test"
                , HistActionType = "i"
                , HistActionDate = new DateTime(2022, 09, 26)
                , Name = "Users"
                , Description = ""
            });
            defaultClaims.Add(new AppClaimType{
                Id = 2
                , Ver = 1
                , HistActionPerformedById = 1
                , HistActionPerformedBy = "admin@demo.test"
                , HistActionType = "i"
                , HistActionDate = new DateTime(2022, 09, 26)
                , Name = "Roles"
                , Description = ""
            });
            defaultClaims.Add(new AppClaimType{
                Id = 3
                , Ver = 1
                , HistActionPerformedById = 1
                , HistActionPerformedBy = "admin@demo.test"
                , HistActionType = "i"
                , HistActionDate = new DateTime(2022, 09, 26)
                , Name = "Companies"
                , Description = ""
            });
        }

        private void populateRoleClaimsAndUpdateRolePermissions()
        {
            var admin = defaultRoles.First(r => r.Name == "Administrator");
            var claims = new List<string>();

            var seedId = 1;
            foreach (var c in defaultClaims)
            {
                defaultRoleClaims.Add(new AppRoleClaim{
                    Id = seedId
                    , Ver = 1
                    , HistActionPerformedById = 1
                    , HistActionPerformedBy = "admin@demo.test"
                    , HistActionType = "i"
                    , HistActionDate = new DateTime(2022, 09, 26)
                    , AppRoleId = admin.Id
                    , AppRoleVer = admin.Ver
                    , ClaimType = c.Name
                    , ClaimValue = ""
                });
                claims.Add(c.Name);
                seedId++;
            }

            admin.Claims = claims;
            admin.Permissions = string.Join(", ", claims);
        }

        private void populateUsers()
        {
            var usr = new AppUser{
                Id = 1
                , Ver = 1
                , HistActionPerformedById = 1
                , HistActionPerformedBy = "admin@demo.test"
                , HistActionType = "i"
                , HistActionDate = new DateTime(2022, 09, 26)
                , UserName = "admin@demo.test"
                , NormalizedUserName = "ADMIN@DEMO.TEST"
                , Email = "admin@demo.test"
                , NormalizedEmail = "ADMIN@DEMO.TEST"
                , EmailConfirmed = true
                , FirstName = "Admin"
                , LastName = "..."
                , RoleName = "Administrator"
                , SecurityStamp = Guid.NewGuid().ToString("D")
            };

            usr.PasswordHash = (new PasswordHasher<AppUser>()).HashPassword(usr, "ad-miniPa55");
            
            defaultUsers.Add(usr);
        }

        private void populateUserRolesAndUpdateUserRole()
        {
            var usrAdmin = defaultUsers.First(u => u.UserName == "admin@demo.test");
            var roleAdmin = defaultRoles.First(r => r.Name == "Administrator");

            defaultUserRoles.Add(new AppUserRole{
                Id = 1
                , Ver = 1
                , HistActionPerformedById = 1
                , HistActionPerformedBy = "admin@demo.test"
                , HistActionType = "i"
                , HistActionDate = new DateTime(2022, 09, 26)
                , AppUserId = usrAdmin.Id
                , AppUserVer = usrAdmin.Ver
                , AppRoleId = roleAdmin.Id
                , AppRoleVer = roleAdmin.Ver
            });

            usrAdmin.RoleName = roleAdmin.Name;
        }
    }
}