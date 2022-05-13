using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using proiect.Controllers;
using System.Data;

namespace proiect.Services
{
    public class UtilizatorRolProvider : RoleProvider
    {
        private DatabaseController dbController = new DatabaseController();

        public UtilizatorRolProvider()
        {
           
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
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

        public override string[] GetRolesForUser(string mail)
        {
            int utilizatorID = 0;
            List<int> roluriID = null;
            List<string> roless = null;
            DataSet Tabela = dbController.SQL(dbController.CautareUserByMail(mail), "Utilizator");
            DataTable dataTable = Tabela.Tables["Utilizator"];
            foreach (DataRow currentRow in dataTable.Rows)
            {
                utilizatorID = Int32.Parse(currentRow["UtilizatorID"] as String);
            }
            Tabela = dbController.SQL(dbController.SelectUtilizatorRolByID(id: utilizatorID), "UtilizatorRol");
            dataTable = Tabela.Tables["UtilizatorRol"];
            foreach (DataRow currentRow in dataTable.Rows)
            {
                roluriID.Add(Int32.Parse(currentRow["RolID"] as String));
            }

            foreach(int rolID in roluriID)
            {
                Tabela = dbController.SQL(dbController.CautareRolByID(rolID), "Rol");
                dataTable = Tabela.Tables["Rol"];
                foreach (DataRow currentRow in dataTable.Rows)
                {
                    roless.Add(currentRow["Nume"] as String);
                }
            }
            string[] roles = roless.ToArray();

            return roles;
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