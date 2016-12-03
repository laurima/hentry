using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using hentry.Models;

namespace hentry
{
    public class CustomRoleProvider : RoleProvider
    {
        private string applicationName;
        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }

            set
            {
                applicationName = value;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();

            using (acsm_ff4a6a83158a8e0Entities db = new acsm_ff4a6a83158a8e0Entities())
            {
                var user = db.aspnetusers.FirstOrDefault(x => x.UserName == username);

                if (user == null)
                {
                    return null;
                }
                else
                {
                    if (db.admin.Count(x => x.user == user.Id) == 1)
                    {
                        roles.Add("admin");
                    }
                    if (db.projectmanager.Count(x => x.user == user.Id) == 1)
                    {
                        roles.Add("projectmanager");
                    }
                    if (db.projectworker.Count(x => x.user == user.Id) == 1)
                    {
                        roles.Add("projectworker");
                    }
                    
                }
            }
            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}